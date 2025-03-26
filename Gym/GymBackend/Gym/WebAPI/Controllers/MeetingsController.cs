using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MeetingsController : ControllerBase
{
    // CREATE --> POST
    // RETRIEVE --> GET
    // UPDATE --> PUT
    // DELETE --> DELETE

    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(Meetings meeting)
    {
        try
        {
            var mm = new MeetingsManager();
            mm.Create(meeting);
            return Ok(meeting);
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
            var mm = new MeetingsManager();
            var meeting = mm.RetrieveById(id);
            if (meeting == null) return NotFound();
            mm.Delete(meeting);
            return Ok(meeting);
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
    public ActionResult Update(Meetings meeting)
    {
        try
        {
            var mm = new MeetingsManager();
            mm.Update(meeting);
            return Ok(meeting);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("CancelMeeting")]
    public ActionResult CancelMeeting(Meetings meeting)
    {
        try
        {
            var mm = new MeetingsManager();
            mm.CancelMeeting(meeting);
            return Ok(meeting);
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
            var mm = new MeetingsManager();
            return Ok(mm.RetrieveAll());
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
            var mm = new MeetingsManager();
            var meeting = mm.RetrieveById(id);
            if (meeting == null) return NotFound();
            return Ok(meeting);
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
            var mm = new MeetingsManager();
            return Ok(mm.RetrieveAllWithName());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}