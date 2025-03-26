using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoutineController : Controller
{
    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(RoutineRequest routineRequest)
    {
        try
        {
            var rm = new RoutineManager();
            rm.Create(routineRequest.Routine, routineRequest.ExerciseIds);
            return Ok(routineRequest);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion

    public class RoutineRequest
    {
        public Routine Routine { get; set; }
        public List<int> ExerciseIds { get; set; }
    }

    #region GETS

    [HttpGet]
    [Route("RetrieveByUser")]
    public ActionResult RetrieveByUser(int clientId)
    {
        try
        {
            var rm = new RoutineManager();
            return Ok(rm.RetrieveByUser(clientId));
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }


    [HttpGet]
    [Route("RetrieveById")]
    public ActionResult RetrieveById(int Id)
    {
        try
        {
            var rm = new RoutineManager();
            return Ok(rm.RetrieveById(Id));
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}