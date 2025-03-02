using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;

using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("/api")]
[ApiController]
public class HabitController : Controller
{
    private readonly IHabitRepo _habitRepo;
    private readonly IUserRepo _userRepo;
    private readonly AuthHelper _authHelper;

    public HabitController(IHabitRepo habitRepo, IUserRepo userRepo, AuthHelper authHelper)
    {
        _habitRepo = habitRepo;
        _userRepo = userRepo;
        _authHelper = authHelper;
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
            LastCheckIn = DateTime.Now,
            IsActive = true,
            UserId = userId
        };

        return Ok(_habitRepo.Create(habit));  
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
        return Ok();
    }

    [HttpPost("habits/{id}/check-in")]
    public IActionResult CheckIn(int id)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var habit = _habitRepo.GetById(id);

        if (habit == null || habit.UserId != userId) return NotFound();

        if (DateTime.UtcNow - habit.LastCheckIn > TimeSpan.FromHours(24))
        {
            habit.CurrentStreak = 1;
        }
        else
        {
            habit.CurrentStreak++;

            if (habit.CurrentStreak > habit.BestStreak) habit.BestStreak = habit.CurrentStreak;
        }

        habit.LastCheckIn = DateTime.UtcNow;
        _habitRepo.Update(habit);

        return Ok(habit);
    }

}
