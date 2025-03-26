using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRolController : ControllerBase
{
    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(UserRole userRole)
    {
        try
        {
            var urm = new UserRolManager();
            urm.Create(userRole);
            return Ok(userRole);
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
            var urm = new UserRolManager();
            // Crear un DTO vacío para la eliminación
            var userRole = new UserRole { Id = id };
            urm.Delete(userRole);
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
            var urm = new UserRolManager();
            return Ok(urm.RetrieveAll());
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
            var urm = new UserRolManager();
            return Ok(urm.RetrieveById(id));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}