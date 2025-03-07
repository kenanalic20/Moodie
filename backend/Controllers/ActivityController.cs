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

        var existingActivity = _repositoryActivity.GetById(a.Id, activities);
        
        if(existingActivity!=null && a.Id!=0) {
            existingActivity.MoodId = a.MoodId;
            existingActivity.Mood = _repositoryMood.GetById(a.MoodId);

            if(!string.IsNullOrEmpty(a.Name)){
                existingActivity.Name = a.Name;
            }

            if(!string.IsNullOrEmpty(a.Description)){
                existingActivity.Description = a.Description;
            }
        
            return Created("success", _repositoryActivity.Update(existingActivity));
        }

        if(activities.Count>=10){ //this can be added in settings for example disable activity limit or change the limit
            return BadRequest("You can only have 10 activities");
        }

        if(string.IsNullOrEmpty(a.Name)){
            return BadRequest("Name is required");
        }

        var activity = new Activity
        {
            Name=a.Name,
            Description = a.Description,
            UserId = userId,
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

    [HttpGet("mood/activities/best")]
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

    [HttpGet("mood/activities/worst")]
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
