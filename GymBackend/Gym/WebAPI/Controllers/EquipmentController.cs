using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController : ControllerBase
{
    //CREATE --> POST
    //REATRIVE --> GET
    //UPDATE --> PUT
    //DELETE --> DELETE

    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(Equipment equipment)
    {
        try
        {
            var em = new EquipmentManager();
            em.Create(equipment);
            return Ok(equipment);
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
    public ActionResult Update(Equipment equipment)
    {
        try
        {
            var em = new EquipmentManager();
            em.Update(equipment);
            return Ok(equipment);
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
    public ActionResult Delete(Equipment equipment)
    {
        try
        {
            var em = new EquipmentManager();
            em.Delete(equipment);
            return Ok(equipment);
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
            var em = new EquipmentManager();
            return Ok(em.RetrieveAll());
        }
        catch (Exception ex)
        {
            //500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveById")]
    public ActionResult RetrieveById(int id)
    {
        try
        {
            var em = new EquipmentManager();
            return Ok(em.RetrieveById(id));
        }
        catch (Exception ex)
        {
            //500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}