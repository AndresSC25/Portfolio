using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonalTrainingController : ControllerBase
{
    // CREATE --> POST
    // RETRIEVE --> GET
    // UPDATE --> PUT
    // DELETE --> DELETE

    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(PersonalTraining personalTraining)
    {
        try
        {
            var ptManager = new PersonalTrainingManager();
            ptManager.Create(personalTraining);
            return Ok(personalTraining);
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
            var ptManager = new PersonalTrainingManager();
            // Crear un DTO vacío para la eliminación
            var personalTraining = new PersonalTraining { Id = id };
            ptManager.Delete(personalTraining);
            return Ok(new { Id = id });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion

    [HttpGet]
    [Route("RetrieveByEmployeeId")]
    public ActionResult RetrieveByEmployeeId(int id)
    {
        try
        {
            var ptManager = new PersonalTrainingManager();
            return Ok(ptManager.RetrieveByEmployeeId(id));
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveByClientId")]
    public ActionResult RetrieveByClientId(int id)
    {
        try
        {
            var ptManager = new PersonalTrainingManager();
            return Ok(ptManager.RetrieveByClientId(id));
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveByClientIdPayable")]
    public ActionResult RetrieveByClientIdPayable(int id)
    {
        try
        {
            var ptManager = new PersonalTrainingManager();
            var personalTraining = ptManager.RetrieveByClientIdPayable(id);
            return Ok(personalTraining);
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    #region PUT

    [HttpPut]
    [Route("Update")]
    public ActionResult Update(PersonalTraining personalTraining)
    {
        try
        {
            var ptManager = new PersonalTrainingManager();
            ptManager.Update(personalTraining);
            return Ok(personalTraining);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("Cancel")]
    public ActionResult Cancel(PersonalTraining personalTraining)
    {
        try
        {
            var ptManager = new PersonalTrainingManager();
            ptManager.Cancel(personalTraining);
            return Ok(personalTraining);
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
            var ptManager = new PersonalTrainingManager();
            return Ok(ptManager.RetrieveAll());
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveById")]
    public ActionResult RetrieveById(int id)
    {
        try
        {
            var ptManager = new PersonalTrainingManager();
            return Ok(ptManager.RetrieveById(id));
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}