using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;
using Moodie.Dtos;
using System.Collections.Generic;

namespace Moodie.Controllers;

[Route("/api")]
[ApiController]
public class HabitController : Controller
{
    private readonly IHabitRepo _habitRepo;
    private readonly IUserRepo _userRepo;
    private readonly AuthHelper _authHelper;
    private readonly IAchievementRepo _achievementRepo;
    private readonly TimeZoneInfo _bosnianTimeZone;

    public HabitController(IHabitRepo habitRepo, IUserRepo userRepo, AuthHelper authHelper, IAchievementRepo achievementRepo)
    {
        _habitRepo = habitRepo;
        _userRepo = userRepo;
        _authHelper = authHelper;
        _achievementRepo = achievementRepo;
        _bosnianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
    }

    [HttpPost("habits")]
    public IActionResult CreateHabit(HabitDto dto)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var habit = new Habit
        {
            Name = dto.Name,
            Description = dto.Description,
            CurrentStreak = 0,
            BestStreak = 0,
            LastCheckIn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _bosnianTimeZone),
            IsActive = true,
            UserId = userId
        };

        var createdHabit = _habitRepo.Create(habit);
        
        var newAchievements = CheckAndAwardHabitAchievements(userId);
        
        var result = new 
        {
            habit = createdHabit,
            newAchievements = newAchievements
        };

        return Ok(result);
    }

    [HttpGet("habits")]
    public IActionResult GetHabits()
    {
      
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        return Ok(_habitRepo.GetByUserId(userId));
    }

    [HttpPut("habits/{id}")]
    public IActionResult Update(int id, HabitDto dto)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var habit = _habitRepo.GetById(id);

        if (habit == null || habit.UserId != userId) return NotFound();

        habit.Name = dto.Name;
        habit.Description = dto.Description;
        _habitRepo.Update(habit);

        return Ok(habit);  
    }

    [HttpDelete("habits/{id}")]
    public IActionResult Delete(int id)
    {
        
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var habit = _habitRepo.GetById(id);

        if (habit == null || habit.UserId != userId)return NotFound();

        _habitRepo.Delete(id);
        return NoContent();
    }

    [HttpGet("habits/{id}")]
    public IActionResult GetHabit(int id)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var habit = _habitRepo.GetById(id);

        if (habit == null || habit.UserId != userId) return NotFound();

        return Ok(habit);
    }

    [HttpPost("habits/{id}/check-in")]
    public IActionResult CheckIn(int id)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var habit = _habitRepo.GetById(id);

        if (habit == null || habit.UserId != userId) return NotFound();

        var bosnianNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _bosnianTimeZone);
        
        var lastCheckInDate = habit.LastCheckIn.Date;
        var bosnianToday = bosnianNow.Date;

        if (lastCheckInDate < bosnianToday)
        {
            habit.CurrentStreak++;
            if (habit.CurrentStreak > habit.BestStreak) habit.BestStreak = habit.CurrentStreak;
        }
        else if ((bosnianNow - habit.LastCheckIn).TotalHours > 24)
        {
            habit.CurrentStreak = 1;
        }

        habit.LastCheckIn = bosnianNow;
        _habitRepo.Update(habit);

        return Ok(habit);
    }

    private List<UserAchievement> CheckAndAwardHabitAchievements(int userId)
    {
        var newAchievements = new List<UserAchievement>();
        
        if (!_achievementRepo.HasUserEarnedAchievement(userId, "added_habit"))
        {
            var achievement = _achievementRepo.AddUserAchievement(userId, "added_habit");
            if (achievement != null) newAchievements.Add(achievement);
        }
        
        return newAchievements;
    }
}
