using auth.Helper;
using auth.Models;
using Dtos.MoodDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moodie.Data;


namespace Moodie.Controllers;
[Route("api")]
[ApiController]
public class ActivityController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly JWTService _jwtService;
    private readonly IUserRepo _repositoryUser;
    private readonly IActivityRepo _activityRepo;
    public ActivityController(ApplicationDbContext context, IUserRepo repositoryUser,
        JWTService jwtService, IActivityRepo activityRepo)
    {
        _context = context;
        _jwtService = jwtService;
        _activityRepo = activityRepo;
        _repositoryUser = repositoryUser;
    }
    [HttpPost("mood/activities")]
    public IActionResult AddActivities(ActivityDto a)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
            var user = _repositoryUser.GetById(userId);
            if(a.Name==null){
                return BadRequest();
            }
            var activity = new Activity
            {
                Name=a.Name,
                Description = a.Description,
                UserId = userId
            };
            return Created("success", _activityRepo.Create(activity));
        }
        catch (SecurityTokenException ex)
        {
            return Unauthorized("Invalid or expired token.");
        }
        catch (Exception e) // Catch any other unexpected exception
        {
            return StatusCode(500, "An error occurred.");
        }

    }
    [HttpGet("mood/activities")]
    public IActionResult GetActivitiesByUserId()
    {
        try
        {
            //get users moods from db based on userid
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
            var activities=_activityRepo.GetByUserId(userId);
            return Ok(activities);
            
        }
        catch (SecurityTokenException ex) // Catch the specific exception type
        {
            return Unauthorized("Invalid or expired token.");
        }
        catch (Exception e) // Catch any other unexpected exception
        {
            return StatusCode(500, "An error occurred.");
        }
    }
    [HttpDelete("mood/activities/{id}")]
    public IActionResult DeleteActivity(int id){
      
        _activityRepo.Delete(id);
       return Ok(id);
         
    }

}
