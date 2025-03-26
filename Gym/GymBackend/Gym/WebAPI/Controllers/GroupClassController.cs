using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupClassController : ControllerBase
{
    // CREATE --> POST
    // RETRIEVE --> GET
    // UPDATE --> PUT
    // DELETE --> DELETE

    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(GroupClass groupClass)
    {
        try
        {
            var gcManager = new GroupClassManager();
            gcManager.Create(groupClass);
            return Ok(groupClass);
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
    public ActionResult Update(GroupClass groupClass)
    {
        try
        {
            var gcManager = new GroupClassManager();
            gcManager.Update(groupClass);
            return Ok(groupClass);
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
            var gcManager = new GroupClassManager();
            var groupClass = gcManager.RetrieveById(id);
            if (groupClass == null) return NotFound();
            gcManager.Delete(groupClass);
            return Ok(groupClass);
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
            var gcManager = new GroupClassManager();
            return Ok(gcManager.RetrieveAll());
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
            var gcManager = new GroupClassManager();
            var groupClass = gcManager.RetrieveById(id);
            if (groupClass == null) return NotFound();
            return Ok(groupClass);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveAllWithName")]
    public ActionResult RetrieveAllWithName()
    {
        try
        {
            var gcManager = new GroupClassManager();
            return Ok(gcManager.RetrieveAllWithName());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveAvailableWithName")]
    public ActionResult RetrieveAvailableWithName()
    {
        try
        {
            var gcManager = new GroupClassManager();
            var AvailableClasses = gcManager.RetrieveAvailableWithName();
            return Ok(AvailableClasses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}