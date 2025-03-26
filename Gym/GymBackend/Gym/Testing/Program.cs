using DataAccess.CRUD;
using DTOs;

public class InvoiceEmailSenderTest
{
    public static async Task Main(string[] args)
    {
        var invoiceCrudFactory = new InvoiceCrudFactory();
        var detailCrudFactory = new DetailCrudFactory();
        var userCrudFactory = new UserCrudFactory();

        // Usuario sin Clase Personal

        var invoiceTest = invoiceCrudFactory.RetrieveById<Invoice>(27);
        var detailTest = detailCrudFactory.RetrieveById<Detail>(18);
        var userTest = userCrudFactory.RetrieveById<User>(6);
        var detailList = new List<Detail>();
        detailList.Add(detailTest);

        // Usuario con Clase Personal

        var invoiceTestCP = invoiceCrudFactory.RetrieveById<Invoice>(28);
        var detailTestCP1 = detailCrudFactory.RetrieveById<Detail>(19);
        var detailTestCP2 = detailCrudFactory.RetrieveById<Detail>(20);
        var userTestCP = userCrudFactory.RetrieveById<User>(2);
        var detailListCP = new List<Detail>();
        detailListCP.Add(detailTestCP1);
        detailListCP.Add(detailTestCP2);

        invoiceTest.MembershipID = 1;

        invoiceTestCP.MembershipID = 3;


        // Step 3: Instantiate the InvoiceEmailSender
        var emailSender = new InvoiceEmailSender();

        // Step 4: Send the invoice email asynchronously
        await emailSender.SendInvoiceEmailAsync(invoiceTestCP, detailListCP, userTestCP);

        // Step 5: Confirm email was sent (you can check the email inbox to verify)
        Console.WriteLine("Invoice email sent. Please check your inbox.");
    }
}