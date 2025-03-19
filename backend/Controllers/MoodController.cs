using Moodie.Helper;
using Moodie.Models;
using Moodie.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;
using Moodie.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class MoodController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMoodRepo _repositoryMood;
    private readonly IUserRepo _repositoryUser;
    private readonly IAchievementRepo _repositoryAchievement;
    private readonly AuthHelper _authHelper;

    public MoodController(ApplicationDbContext context, IUserRepo repositoryUser,
        AuthHelper authHelper, IMoodRepo repositoryMood, IAchievementRepo repositoryAchievement)
    {
        _context = context;
        _repositoryUser = repositoryUser;
        _repositoryMood = repositoryMood;
        _repositoryAchievement = repositoryAchievement;
        _authHelper = authHelper;
    }

    [HttpPost("mood")]
    public IActionResult AddMood(MoodDto moodDto)
    {  
        if(!_authHelper.IsUserLoggedIn(Request,out var userId)) return Unauthorized("Invalid or expired token.");

        var user = _repositoryUser.GetById(userId);

        var mood = new Mood
        {
            MoodValue = (int)moodDto.MoodValue,  
            Date = moodDto.Date ?? DateTime.Now,
            User = user,
            UserId = userId,
        };

        var createdMood = _repositoryMood.Create(mood);
        
       
        var userAchievements = CheckAndAwardMoodAchievements(userId);        
        
        var result = new 
        {
            mood = createdMood,
            newAchievements = userAchievements
        };

        return Created("success", result);
    }

    [HttpGet("mood")]
    public IActionResult GetMoods()
    {
        if(!_authHelper.IsUserLoggedIn(Request,out var userId)) return Unauthorized("Invalid or expired token.");
        
        var moods = _repositoryMood.GetByUserId(userId);
        
                var moodWithActivities = moods.Select(mood => {
                    var activities = _context.MoodActivities
                        .Where(ma => ma.MoodId == mood.Id)
                        .Include(ma => ma.Activity)
                        .Select(ma => new {
                            Id = ma.Activity.Id,
                            Name = ma.Activity.Name,
                            Description = ma.Activity.Description
                        })
                        .ToList();

                    return new {
                        Id = mood.Id,
                        MoodValue = mood.MoodValue,
                        Date = mood.Date,
                        UserId = mood.UserId,
                        Activities = activities
                    };
                }).ToList();
        var moodWithNotesAndActivities = moods.Select(mood => {
            var notes = _context.Notes
                .Where(note => note.UserId == userId && note.MoodId == mood.Id)
                .Select(note => new {
                    Id = note.Id,
                    Title = note.Title,
                    Description = note.Description,
                    Image = note.ImagePath != null ? note.ImagePath : null
                })
                .ToList();

            var activities = _context.MoodActivities
                .Where(ma => ma.MoodId == mood.Id)
                .Include(ma => ma.Activity)
                .Select(ma => new {
                    Id = ma.Activity.Id,
                    Name = ma.Activity.Name,
                    Description = ma.Activity.Description
                })
                .ToList();

            return new {
                Id = mood.Id,
                MoodValue = mood.MoodValue,
                Date = mood.Date,
                UserId = mood.UserId,
                Notes = notes,
                MoodActivities = activities
            };
        }).ToList();
        

        return Ok(moodWithNotesAndActivities);
    }

    [HttpDelete("mood/{id}")]
    public IActionResult DeleteMoods(int id) {
        if(!_authHelper.IsUserLoggedIn(Request,out var userId)) return Unauthorized("Invalid or expired token.");

        _repositoryMood.Delete(id);

        return NoContent();
    }
    
    private List<UserAchievement> CheckAndAwardMoodAchievements(int userId)
    {
        var moodCount = _repositoryMood.GetByUserId(userId).Count;
        var newAchievements = new List<UserAchievement>();
                
        if (moodCount >= 1)
        {
            if (!_repositoryAchievement.HasUserEarnedAchievement(userId, "1_mood"))
            {
                var achievement = _repositoryAchievement.AddUserAchievement(userId, "1_mood");
                if (achievement != null) newAchievements.Add(achievement);
            }
        }
        
        if (moodCount >= 10)
        {
            if (!_repositoryAchievement.HasUserEarnedAchievement(userId, "10_mood"))
            {
                var achievement = _repositoryAchievement.AddUserAchievement(userId, "10_mood");
                if (achievement != null) newAchievements.Add(achievement);
            }
        }
        
        if (moodCount >=50 )
        {
            if (!_repositoryAchievement.HasUserEarnedAchievement(userId, "50_mood"))
            {
                var achievement = _repositoryAchievement.AddUserAchievement(userId, "50_mood");
                if (achievement != null) newAchievements.Add(achievement);
            }
        }
        
        return newAchievements;
    }
}
