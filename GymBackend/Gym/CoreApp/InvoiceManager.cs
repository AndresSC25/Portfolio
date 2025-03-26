using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class InvoiceManager
{
    public void Create(Invoice invoice)
    {
        var iCrud = new InvoiceCrudFactory();
        var dCrud = new DetailCrudFactory();
        var dstCrud = new DiscountCrudFactory();
        var umsCrud = new UserMembershipCrud();
        var msCrud = new MembershipCrudFactory();
        var ptManager = new PersonalTrainingManager();
        var uCrud = new UserCrudFactory();
        var detailsList = new List<Detail>();

        // Validar la factura antes de crearla
        ValidateInvoice(invoice);

        //Validar el descuento que se va a aplicar (Nuevo, expirado, o seleccionado en frontEndd)
        invoice = ValidateDiscount(invoice);

        //Tenemos el invoice que acabamos de crear
        iCrud.Create(invoice);
        var NewestInvoice = iCrud.RetrieveLastestInvoiceByUserId(invoice.UserId);

        //Obtenemos el descuento que vamos a utilizar
        var discount = dstCrud.RetrieveById<Discount>(NewestInvoice.DiscountId);

        //Obtenemos el Membership elegido
        var membership =
            msCrud.RetrieveById<Membership>(invoice.MembershipID.HasValue ? invoice.MembershipID.Value : 0);

        //Obtenemos las clases que se deben pagar
        var ptPayable = ptManager.RetrieveByClientIdPayable(NewestInvoice.UserId);

        //Crear el UserMembership nuevo
        var umNew = new UserMembership
        {
            UserId = invoice.UserId,
            MembershipId = invoice.MembershipID.HasValue ? invoice.MembershipID.Value : 0,
            Created = DateTime.Now
        };
        umsCrud.Create(umNew);

        //Llamo el UserMembership que acabo de crear para obtener el ID
        var lstUM = umsCrud.RetrieveAll<UserMembership>();
        var newUM = obtainNewUMbyID(invoice.UserId, lstUM);


        //Crear de details para Membership y PTs
        var MembershipDetail = new Detail
        {
            InvoiceId = NewestInvoice.Id, // Asigna el ID de la factura correspondiente
            UserMembershipId = newUM.Id, // Asigna el ID de la membresía del usuario (puede ser null)
            PersonalTrainingId = 2, // Esto es un dummy solo para rellenar
            Price = membership.MonthlyCost, // Asigna el precio correspondiente
            Created = DateTime.Now
        };
        dCrud.Create(MembershipDetail);
        detailsList.Add(MembershipDetail);


        //Guardo el costo del membership
        var membershipCost = membership.MonthlyCost;


        //Acumulo el costo del personaltraining
        double ptCost = 0;

        foreach (var personalTraining in ptPayable)
        {
            var inTime = personalTraining.TimeOfEntry;
            var outTime = personalTraining.TimeOfExit;
            var totalHour = (outTime - inTime).TotalHours;
            var cost = totalHour * personalTraining.HourlyRate;
            ptCost += cost;
            var ptDetail = new Detail
            {
                InvoiceId = NewestInvoice.Id, // Asigna el ID de la factura correspondiente
                UserMembershipId = 2, // Dummy para rellenar
                PersonalTrainingId = personalTraining.Id, // Asigna el ID del entrenamiento personal (puede ser null)
                Price = cost, // Asigna el precio correspondiente
                Created = DateTime.Now
            };
            dCrud.Create(ptDetail);
            detailsList.Add(ptDetail);
        }

        //Update a PTs que ya se cancelaron
        foreach (var personalTraining in ptPayable)
        {
            personalTraining.IsPaid = "si";
            ptManager.Update(personalTraining);
        }

        //Update al invoice con los cobros
        NewestInvoice.Amount = ptCost + membershipCost;
        NewestInvoice.AmountAfterDiscount = ptCost + membershipCost * (1 - (double)discount.Percentage / 100);
        iCrud.Update(NewestInvoice);


        //Update de status cliente a activo
        var user = uCrud.RetrieveById<User>(invoice.UserId);
        user.Status = "active";
        uCrud.Update(user);

        //Aqui iria la logica para enviar el correo con PDF y XML del pago
        NewestInvoice.MembershipID = invoice.MembershipID;
        var ies = new InvoiceEmailSender();
        var sendEmailTask = ies.SendInvoiceEmailAsync(NewestInvoice, detailsList, user);
    }

    private UserMembership obtainNewUMbyID(int userId, List<UserMembership> lstUM)
    {
        // Filtrar la lista para encontrar las membresías del usuario especificado
        var userMemberships = lstUM.Where(um => um.UserId == userId);

        // Retornar la membresía más reciente basada en la fecha de creación
        // (asumiendo que la membresía más reciente es la última creada)
        var newUM = userMemberships.OrderByDescending(um => um.Created).FirstOrDefault();

        if (newUM == null)
            // Manejar el caso cuando no se encuentra ninguna membresía para el usuario
            throw new Exception($"No se encontró ninguna membresía para el usuario con ID {userId}.");
        return newUM;
    }

    public void Update(Invoice invoice)
    {
        var iCrud = new InvoiceCrudFactory();
        iCrud.Update(invoice);
    }

    public void Delete(Invoice invoice)
    {
        var iCrud = new InvoiceCrudFactory();
        iCrud.Delete(invoice);
    }

    public List<Invoice> RetrieveAll()
    {
        var iCrud = new InvoiceCrudFactory();
        return iCrud.RetrieveAll<Invoice>();
    }

    public List<Invoice> RetrieveAllwithDetails()
    {
        var iCrud = new InvoiceCrudFactory();
        return iCrud.RetrieveAllwithDetails();
    }

    public List<Invoice> RetrieveByClientIdwithDetails(int id)
    {
        var allInvoices = RetrieveAllwithDetails();
        var ret = new List<Invoice>();
        foreach (var invoice in allInvoices)
            if (invoice.UserId == id)
                ret.Add(invoice);

        return ret;
    }

    public Invoice RetrieveById(int id)
    {
        var iCrud = new InvoiceCrudFactory();
        return iCrud.RetrieveById<Invoice>(id);
    }

    #region Validations

    private void ValidateInvoice(Invoice invoice)
    {
        if (invoice.MembershipID == 0) throw new Exception("Por favor selecciona un tipo de membresía.");
        if (invoice.UserId <= 0) throw new Exception("Por favor selecciona un usuario.");
        if (invoice.PaymentMethod == "empty") throw new Exception("Por favor selecciona un método de pago.");
    }

    private Invoice ValidateDiscount(Invoice invoice)
    {
        var msManager = new UserMembershipManager();
        var NewestMembership = msManager.RetrieveNewestByUserId(invoice.UserId);

        var mDate = NewestMembership.Created;
        var ThreeMonthsAgo = DateTime.Now.AddMonths(-3);
        if (invoice.DiscountId == -1)
        {
            if (NewestMembership.Id == 0 && NewestMembership.UserId == 0)
                invoice.DiscountId = 0;
            else if (mDate < ThreeMonthsAgo)
                invoice.DiscountId = 1;
            else
                invoice.DiscountId = 7;
        }

        return invoice;
    }

    #endregion
}