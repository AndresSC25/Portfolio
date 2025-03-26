using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(Discount discount)
    {
        try
        {
            var dm = new DiscountManager();
            dm.Create(discount);
            return Ok(discount);
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
    public ActionResult Update(Discount discount)
    {
        try
        {
            var dm = new DiscountManager();
            dm.Update(discount);
            return Ok(discount);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion

    #region DELETE

    [HttpDelete]
    [Route("Delete/{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var dm = new DiscountManager();
            var discount = new Discount { Id = id };
            dm.Delete(discount);
            return Ok(new { Id = id });
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
            var dm = new DiscountManager();
            return Ok(dm.RetrieveAll());
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
            var dm = new DiscountManager();
            return Ok(dm.RetrieveById(id));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}