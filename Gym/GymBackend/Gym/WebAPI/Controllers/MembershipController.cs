using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembershipController : ControllerBase
{
    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(Membership membership)
    {
        try
        {
            var meCrud = new MembershipManager();
            meCrud.Create(membership);
            return Ok(membership);
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
    public ActionResult Update(Membership membership)
    {
        try
        {
            var meCrud = new MembershipManager();
            meCrud.Update(membership);
            return Ok(membership);
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
            var meCrud = new MembershipManager();
            var membership = new Membership { Id = id };
            meCrud.Delete(membership);
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
            var meCrud = new MembershipManager();
            return Ok(meCrud.RetrieveAll());
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
            var meCrud = new MembershipManager();
            return Ok(meCrud.RetrieveById(id));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}