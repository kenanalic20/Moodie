using Moodie.Helper;
using Moodie.Models;
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
    private readonly IUserRepo _repositoryUser;
    private readonly IActivityRepo _activityRepo;
    private readonly AuthHelper _authHelper;
    public ActivityController(ApplicationDbContext context, IUserRepo repositoryUser,
        AuthHelper authHelper, IActivityRepo activityRepo)
    {
        _context = context;
        _activityRepo = activityRepo;
        _repositoryUser = repositoryUser;
        _authHelper = authHelper;

    }

    [HttpPost("mood/activities")]
    public IActionResult AddActivities(ActivityDto a)
    {
      
        if(!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
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

    [HttpGet("mood/activities")]
    public IActionResult GetActivitiesByUserId()
    {
        if(!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
    
        var activities=_activityRepo.GetByUserId(userId);

        return Ok(activities);
    }

    [HttpDelete("mood/activities/{id}")]
    public IActionResult DeleteActivity(int id){
      
        _activityRepo.Delete(id);

        return Ok(id); 
    }

}
