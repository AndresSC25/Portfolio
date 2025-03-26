using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolController : Controller
{
    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(Rol rol)
    {
        try
        {
            var rm = new RolManager();
            rm.Create(rol);
            return Ok(rol);
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
    public ActionResult Delete(UserRole userRole)
    {
        try
        {
            var rm = new RolManager();
            rm.Delete(userRole.UserId, userRole.RoleId);
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
            var rm = new RolManager();
            return Ok(rm.RetrieveById(id));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpGet]
    [Route("RetrieveByIdByUser")]
    public ActionResult RetrieveByIdUserList(int id)
    {
        try
        {
            var rm = new RolManager();
            return Ok(rm.RetrieveAllRolesByUserId(id));
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
            var rm = new RolManager();
            return Ok(rm.RetrieveAll());
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}