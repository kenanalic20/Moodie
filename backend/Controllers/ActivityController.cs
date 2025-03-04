using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;
using Moodie.Dtos;


namespace Moodie.Controllers;
[Route("api")]
[ApiController]
public class ActivityController : Controller
{
    private readonly IActivityRepo _repositoryActivity;
    private readonly IMoodRepo _repositoryMood;
    private readonly AuthHelper _authHelper;
    public ActivityController(
        AuthHelper authHelper, IActivityRepo repositoryActivity, IMoodRepo repositoryMood)
    {
        _repositoryMood = repositoryMood;
        _repositoryActivity = repositoryActivity;
        _authHelper = authHelper;

    }

    [HttpPost("mood/activities")]
    public IActionResult AddActivities(ActivityDto a)
    {
      
        if(!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        var activities = _repositoryActivity.GetByUserId(userId);

        if(a.Name==null){
            return BadRequest();
        }
        
        if(activities.Count>=10){ //this can be added in settings for example disable activity limit or change the limit
            return BadRequest("You can only have 10 activities");
        }

        var activity = new Activity
        {
            Name=a.Name,
            Description = a.Description,
            MoodId = a.MoodId,
            UserId = userId,
            Mood = _repositoryMood.GetById(a.MoodId)
        };

        return Created("success", _repositoryActivity.Create(activity));
    }

    [HttpGet("mood/activities")]
    public IActionResult GetActivities()
    {
        if(!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
    
        var activities = _repositoryActivity.GetByUserId(userId);

        if(activities.Count==0){
            return NotFound();
        }

        return Ok(activities);
    }

    [HttpGet("mood/best-activities")]
    public IActionResult GetBestActivities()
    {
        if(!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
    
        var averageMoodValue = _repositoryMood.GetAverageMoodValue(userId);

        var activities = _repositoryActivity.GetBestMoodActivities(averageMoodValue,userId);

        if(activities.Count==0){
            return NotFound();
        }

        return Ok(activities);
    }

    [HttpGet("mood/worst-activities")]
    public IActionResult GetWorstActivities()
    {
        if(!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
    
        var averageMoodValue = _repositoryMood.GetAverageMoodValue(userId);
        
        var activities = _repositoryActivity.GetWorstMoodActivities(averageMoodValue,userId);

        if(activities.Count==0){
            return NotFound();
        }

        return Ok(activities);
    }

    [HttpDelete("mood/activities/{id}")]
    public IActionResult DeleteActivity(int id){
      
        _repositoryActivity.Delete(id);

        return Ok(id); 
    }

}
