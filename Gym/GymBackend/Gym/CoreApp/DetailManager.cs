using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class DetailManager
{
    public void Create(Detail detail)
    {
        var detailCrud = new DetailCrudFactory();

        // Validar el detalle antes de crearlo
        ValidateDetail(detail);

        detailCrud.Create(detail);
    }

    public void Update(Detail detail)
    {
        var detailCrud = new DetailCrudFactory();
        detailCrud.Update(detail);
    }

    public void Delete(int id)
    {
        var detailCrud = new DetailCrudFactory();
        var detail = new Detail
        {
            Created = DateTime.Now,
            Id = id,
            InvoiceId = 0,
            Price = 0
        };
        detailCrud.Delete(detail);
    }

    public List<Detail> RetrieveAll()
    {
        var detailCrud = new DetailCrudFactory();
        return detailCrud.RetrieveAll<Detail>();
    }

    public List<Detail> RetrieveByInvoiceId(int invoiceId)
    {
        var detailCrud = new DetailCrudFactory();
        var allDetails = detailCrud.RetrieveAll<Detail>();
        var ret = new List<Detail>();
        foreach (var detail in allDetails)
            if (detail.InvoiceId == invoiceId)
                ret.Add(detail);

        return ret;
    }

    public Detail RetrieveById(int id)
    {
        var detailCrud = new DetailCrudFactory();
        return detailCrud.RetrieveById<Detail>(id);
    }

    #region Validations

    private void ValidateDetail(Detail detail)
    {
        if (detail.InvoiceId <= 0) throw new Exception("Por favor selecciona una factura válida.");

        if (detail.Price <= 0) throw new Exception("El precio debe ser mayor a cero.");

        if (detail.Created == default) throw new Exception("Por favor ingresa una fecha de creación válida.");
    }

    #endregion
}