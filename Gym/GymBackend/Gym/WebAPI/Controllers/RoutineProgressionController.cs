using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoutineProgressionController : Controller
{
    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(Routine_Progression routineProgression)
    {
        try
        {
            var rpm = new RoutineProgressionManager();
            rpm.Create(routineProgression);
            return Ok(routineProgression);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion

    #region GETS

    [HttpGet]
    [Route("RetrieveByRoutineId")]
    public ActionResult RetrieveByRoutineId(int routineId)
    {
        try
        {
            var rpm = new RoutineProgressionManager();
            return Ok(rpm.RetrieveByRoutineId(routineId));
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}