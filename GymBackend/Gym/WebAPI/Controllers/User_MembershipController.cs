using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserMembershipController : Controller
{
    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(UserMembership userMembership)
    {
        try
        {
            var umm = new UserMembershipManager();
            umm.Create(userMembership);
            return Ok(userMembership);
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
            var umm = new UserMembershipManager();
            var userMembership = umm.RetrieveById(id);
            if (userMembership == null) return NotFound();
            umm.Delete(userMembership);
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
            var umm = new UserMembershipManager();
            var userMembership = umm.RetrieveById(id);
            if (userMembership == null) return NotFound();
            return Ok(userMembership);
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
            var umm = new UserMembershipManager();
            return Ok(umm.RetrieveAll());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveByUserId")]
    public ActionResult RetrieveByUserId(int userId)
    {
        try
        {
            var umm = new UserMembershipManager();
            var userMemberships = umm.RetrieveByUserId(userId);
            return Ok(userMemberships);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveNewestByUserId")]
    public ActionResult RetrieveNewestByUserId(int userId)
    {
        try
        {
            var umm = new UserMembershipManager();
            var userNewestMembership = umm.RetrieveNewestByUserId(userId);
            return Ok(userNewestMembership);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveByUserIdStatusChange")]
    public ActionResult RetrieveByUserIdStatusChange(int userId)
    {
        try
        {
            var umm = new UserMembershipManager();
            var userNewestMembership = umm.RetrieveByUserIdStatusChange(userId);
            return Ok(userNewestMembership);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}