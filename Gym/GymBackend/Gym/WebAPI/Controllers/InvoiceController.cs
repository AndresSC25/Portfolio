using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    // CREATE --> POST
    // RETRIEVE --> GET
    // UPDATE --> PUT
    // DELETE --> DELETE

    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(Invoice invoice)
    {
        try
        {
            var im = new InvoiceManager();
            im.Create(invoice);
            return Ok(invoice);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion

    #region PUT

    [HttpPut]
    [Route("Update")]
    public ActionResult Update(Invoice invoice)
    {
        try
        {
            var im = new InvoiceManager();
            im.Update(invoice);
            return Ok(invoice);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion

    #region DELETE

    [HttpDelete]
    [Route("Delete")]
    public ActionResult Delete(int id)
    {
        try
        {
            var im = new InvoiceManager();
            var invoice = im.RetrieveById(id);
            if (invoice == null) return NotFound();
            im.Delete(invoice);
            return Ok(invoice);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion

    #region GETS

    [HttpGet]
    [Route("RetrieveAll")]
    public ActionResult RetrieveAll()
    {
        try
        {
            var im = new InvoiceManager();
            return Ok(im.RetrieveAll());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveAllwithDetails")]
    public ActionResult RetrieveAllwithDetails()
    {
        try
        {
            var im = new InvoiceManager();
            var invoices = im.RetrieveAllwithDetails();
            return Ok(invoices);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveByClientIdwithDetails")]
    public ActionResult RetrieveByClientIdwithDetails(int id)
    {
        try
        {
            var im = new InvoiceManager();
            var invoices = im.RetrieveByClientIdwithDetails(id);
            return Ok(invoices);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveById")]
    public ActionResult RetrieveById(int id)
    {
        try
        {
            var im = new InvoiceManager();
            var invoice = im.RetrieveById(id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}