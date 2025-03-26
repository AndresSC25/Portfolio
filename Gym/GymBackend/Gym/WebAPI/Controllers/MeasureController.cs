using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MeasuresController : Controller
{
    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(Measures measure)
    {
        try
        {
            var mm = new MeasuresManager();
            mm.Create(measure);
            return Ok(measure);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion

    #region Delete

    [HttpDelete]
    [Route("Delete")]
    public ActionResult Delete(int id)
    {
        try
        {
            var mm = new MeasuresManager();
            var measure = mm.RetrieveById(id);
            if (measure == null) return NotFound();
            mm.Delete(measure);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion

    #region GETS

    [HttpGet]
    [Route("RetrieveById")]
    public ActionResult RetrieveById(int id)
    {
        try
        {
            var mm = new MeasuresManager();
            var measure = mm.RetrieveById(id);
            if (measure == null) return NotFound();
            return Ok(measure);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveAll")]
    public ActionResult RetrieveAll()
    {
        try
        {
            var mm = new MeasuresManager();
            return Ok(mm.RetrieveAll());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveByUserId")]
    public ActionResult RetrieveByUserId(int id)
    {
        try
        {
            var mm = new MeasuresManager();
            var measures = mm.RetrieveByUserId(id);
            return Ok(measures);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}