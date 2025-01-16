using Moodie.Helper;
using Moodie.Models;
using Dtos.MoodDto;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;


namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class MoodController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMoodRepo _repositoryMood;
    private readonly IUserRepo _repositoryUser;
    private readonly AuthHelper _authHelper;

    public MoodController(ApplicationDbContext context, IUserRepo repositoryUser,
        AuthHelper authHelper, IMoodRepo repositoryMood)
    {
        _context = context;
        _repositoryUser = repositoryUser;
        _repositoryMood = repositoryMood;
        _authHelper = authHelper;
    }

    [HttpPost("mood")]
    public IActionResult AddMood(MoodDto moodDto)
    {  
        if(!_authHelper.IsUserLoggedIn(Request,out var userId)) return Unauthorized("Invalid or expired token.");

        var user = _repositoryUser.GetById(userId);

        Activity activity = null;

        if (moodDto.ActivityId != null) activity = _context.Activity.Find(moodDto.ActivityId);
        
        var mood = new Mood
        {
            MoodValue = moodDto.MoodValue,
            Date = DateTime.Now,
            User = user,
            UserId = userId,
        };

        if (activity == null) return Created("success", _repositoryMood.Create(mood));
        
        mood.Activity = activity;
        mood.ActivityId = activity.Id;

        return Created("success", _repositoryMood.Create(mood));
    }

    [HttpGet("mood")]
    public IActionResult GetMoods()
    {
        if(!_authHelper.IsUserLoggedIn(Request,out var userId)) return Unauthorized("Invalid or expired token.");
        
        var moods = _repositoryMood.GetByUserId(userId);

        return Ok(moods);
    }
    
    
}