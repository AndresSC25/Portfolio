using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DetailController : ControllerBase
{
    // CREATE --> POST
    // RETRIEVE --> GET
    // UPDATE --> PUT
    // DELETE --> DELETE

    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(Detail detail)
    {
        try
        {
            var dm = new DetailManager();
            dm.Create(detail);
            return Ok(detail);
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
    public ActionResult Update(Detail detail)
    {
        try
        {
            var dm = new DetailManager();
            dm.Update(detail);
            return Ok(detail);
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
            var dm = new DetailManager();

            dm.Delete(id);
            return Ok(id);
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
            var dm = new DetailManager();
            return Ok(dm.RetrieveAll());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveByInvoiceId")]
    public ActionResult RetrieveByInvoiceId(int invoiceId)
    {
        try
        {
            var dm = new DetailManager();
            var list = dm.RetrieveByInvoiceId(invoiceId);
            return Ok(list);
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
            var dm = new DetailManager();
            var detail = dm.RetrieveById(id);
            if (detail == null) return NotFound();
            return Ok(detail);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}