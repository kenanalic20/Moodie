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
            MoodValue = (int)moodDto.MoodValue,  // Add explicit cast here
            Date = moodDto.Date ?? DateTime.Now,
            User = user,
            UserId = userId,
        };

        var createdMood = _repositoryMood.Create(mood);
        
        // Check for achievements
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
        
        // Get notes for each mood
        var moodWithNotes = moods.Select(mood => {
            var notes = _context.Notes
                .Where(note => note.UserId == userId && note.MoodId == mood.Id)
                .Select(note => new {
                    Id = note.Id,
                    Title = note.Title,
                    Description = note.Description,
                    Image = note.Image != null ? $"https://localhost:8001/api/images/{note.Image}" : null
                })
                .ToList();
                
            return new {
                Id = mood.Id,
                MoodValue = mood.MoodValue,
                Date = mood.Date,
                UserId = mood.UserId,
                Notes = notes
            };
        }).ToList();

        return Ok(moodWithNotes);
    }
    
    private List<UserAchievement> CheckAndAwardMoodAchievements(int userId)
    {
        var moodCount = _repositoryMood.GetByUserId(userId).Count;
        var newAchievements = new List<UserAchievement>();
                
        // Check for first mood achievement
        if (moodCount >= 1)
        {
            if (!_repositoryAchievement.HasUserEarnedAchievement(userId, "1_mood"))
            {
                var achievement = _repositoryAchievement.AddUserAchievement(userId, "1_mood");
                if (achievement != null) newAchievements.Add(achievement);
            }
        }
        
        // Check for 10 moods achievement
        if (moodCount >= 10)
        {
            if (!_repositoryAchievement.HasUserEarnedAchievement(userId, "10_mood"))
            {
                var achievement = _repositoryAchievement.AddUserAchievement(userId, "10_mood");
                if (achievement != null) newAchievements.Add(achievement);
            }
        }
        
        // Check for 50 moods achievement
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
