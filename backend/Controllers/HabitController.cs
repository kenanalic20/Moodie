using auth.Helper;
using auth.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("/api")]
[ApiController]
public class HabitController : Controller
{
    private readonly IHabitRepo _habitRepo;
    private readonly IUserRepo _userRepo;
    private readonly JWTService _jwtService;

    public HabitController(IHabitRepo habitRepo, IUserRepo userRepo, JWTService jwtService)
    {
        _habitRepo = habitRepo;
        _userRepo = userRepo;
        _jwtService = jwtService;
    }

    [HttpPost("habits")]
    public IActionResult CreateHabit(HabitDto dto)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);

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
        catch (Exception)
        {
            return Unauthorized();
        }
    }

    [HttpGet("habits")]
    public IActionResult GetHabits()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);

            return Ok(_habitRepo.GetByUserId(userId));
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }

    [HttpPut("habits/{id}")]
    public IActionResult Update(int id, HabitDto dto)
    {
        try
        {
            var userId = GetUserIdFromToken();
            var habit = _habitRepo.GetById(id);

            if (habit == null || habit.UserId != userId)
                return NotFound();

            habit.Name = dto.Name;
            habit.Description = dto.Description;
            _habitRepo.Update(habit);

            return Ok(habit);
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }

    [HttpDelete("habits/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var userId = GetUserIdFromToken();
            var habit = _habitRepo.GetById(id);

            if (habit == null || habit.UserId != userId)
                return NotFound();

            _habitRepo.Delete(id);
            return Ok();
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }

    [HttpPost("habits/{id}/check-in")]
    public IActionResult CheckIn(int id)
    {
        try
        {
            var userId = GetUserIdFromToken();
            var habit = _habitRepo.GetById(id);

            if (habit == null || habit.UserId != userId)
                return NotFound();

            if (DateTime.UtcNow - habit.LastCheckIn > TimeSpan.FromHours(24))
            {
                habit.CurrentStreak = 1;
            }
            else
            {
                habit.CurrentStreak++;
                if (habit.CurrentStreak > habit.BestStreak)
                    habit.BestStreak = habit.CurrentStreak;
            }

            habit.LastCheckIn = DateTime.UtcNow;
            _habitRepo.Update(habit);

            return Ok(habit);
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }

    private int GetUserIdFromToken()
    {
        var jwt = Request.Cookies["jwt"];
        var token = _jwtService.Verify(jwt);
        return int.Parse(token.Issuer);
    }
}
