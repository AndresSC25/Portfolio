using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExerciseController : ControllerBase
{
    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(Exercise exercise)
    {
        try
        {
            var em = new ExerciseManager();
            em.Create(exercise);
            return Ok(exercise);
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
    public ActionResult Update(Exercise exercise)
    {
        try
        {
            var em = new ExerciseManager();
            em.Update(exercise);
            return Ok(exercise);
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
    public ActionResult Delete(Exercise exercise)
    {
        try
        {
            var em = new ExerciseManager();
            em.Delete(exercise);
            return Ok(exercise);
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
            var em = new ExerciseManager();
            return Ok(em.RetrieveAll());
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}