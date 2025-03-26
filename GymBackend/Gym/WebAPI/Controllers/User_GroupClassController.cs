using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserGroupClassController : ControllerBase
{
    // CREATE --> POST
    // RETRIEVE --> GET
    // UPDATE --> PUT
    // DELETE --> DELETE

    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(UserGroupClass userGroupClass)
    {
        try
        {
            var ugcManager = new UserGroupClassManager();
            ugcManager.Create(userGroupClass);
            return Ok(userGroupClass);
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
    public ActionResult Update(UserGroupClass userGroupClass)
    {
        try
        {
            var ugcManager = new UserGroupClassManager();
            ugcManager.Update(userGroupClass);
            return Ok(userGroupClass);
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
    public ActionResult Delete(UserGroupClass userGroupClass)
    {
        try
        {
            var ugcManager = new UserGroupClassManager();
            ugcManager.Delete(userGroupClass);
            return Ok(userGroupClass);
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
            var ugcManager = new UserGroupClassManager();
            return Ok(ugcManager.RetrieveAll());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveByGroupClassWithName")]
    public ActionResult RetrieveByGroupClassWithName(int groupClassId)
    {
        try
        {
            var ugcManager = new UserGroupClassManager();
            var lstugc = ugcManager.RetrieveByGroupClassWithName(groupClassId);
            return Ok(lstugc);
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
            var ugcManager = new UserGroupClassManager();
            return Ok(ugcManager.RetrieveById(id));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}