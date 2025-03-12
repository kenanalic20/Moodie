using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;
using Moodie.Dtos;
using System.Collections.Generic;

namespace Moodie.Controllers;
[Route("api")]
[ApiController]
public class ActivityController : Controller
{
    private readonly IActivityRepo _repositoryActivity;
    private readonly AuthHelper _authHelper;
    private readonly IAchievementRepo _achievementRepo;
    
    public ActivityController(
        AuthHelper authHelper, IActivityRepo repositoryActivity, IMoodRepo repositoryMood, IAchievementRepo achievementRepo)
    {
        _repositoryActivity = repositoryActivity;
        _authHelper = authHelper;
        _achievementRepo = achievementRepo;
    }

    [HttpPost("mood/activities")]
    public IActionResult AddActivities(ActivityDto a)
    {
      
        if(!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        var activities = _repositoryActivity.GetByUserId(userId);

        var existingActivity = _repositoryActivity.GetById(a.Id);
        
        if(existingActivity!=null && a.Id!=0) {

            if(!string.IsNullOrEmpty(a.Name)){
                existingActivity.Name = a.Name;
            }

            if(!string.IsNullOrEmpty(a.Description)){
                existingActivity.Description = a.Description;
            }
        
            return Created("success", _repositoryActivity.Update(existingActivity));
        }

        if(activities.Count>=20){ //this can be added in settings for example disable activity limit or change the limit
            return BadRequest("You can only have 20 activities");
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

        var createdActivity = _repositoryActivity.Create(activity);
        
        var newAchievements = CheckAndAwardActivityAchievements(userId);
        
        var result = new 
        {
            activity = createdActivity,
            newAchievements = newAchievements
        };

        return Created("success", result);
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

    [HttpDelete("mood/activities/{id}")]
    public IActionResult DeleteActivity(int id){
        if(!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        _repositoryActivity.Delete(id);

        return Ok(id); 
    }

    private List<UserAchievement> CheckAndAwardActivityAchievements(int userId)
    {
        var newAchievements = new List<UserAchievement>();
        
        // Check for first activity achievement
        if (!_achievementRepo.HasUserEarnedAchievement(userId, "added_activity"))
        {
            var achievement = _achievementRepo.AddUserAchievement(userId, "added_activity");
            if (achievement != null) newAchievements.Add(achievement);
        }
        
        return newAchievements;
    }
}
