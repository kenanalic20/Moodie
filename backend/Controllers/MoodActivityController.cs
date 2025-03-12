using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;
using Moodie.Dtos;
using System.Collections.Generic;

namespace Moodie.Controllers;
[Route("api")]
[ApiController]
public class MoodActivityController : Controller
{
    private readonly IActivityRepo _repositoryActivity;
    private readonly IMoodRepo _repositoryMood;
    private readonly AuthHelper _authHelper;
    private readonly IMoodActivityRepo _repositoryMoodActivity;

    public MoodActivityController(
        AuthHelper authHelper,
        IActivityRepo repositoryActivity,
        IMoodRepo repositoryMood,
        IMoodActivityRepo repositoryMoodActivity
        )
    {
        _repositoryMood = repositoryMood;
        _repositoryActivity = repositoryActivity;
        _authHelper = authHelper;
        _repositoryMoodActivity = repositoryMoodActivity;
    }

    [HttpPost("moodactivity")]
    public IActionResult CreateMoodActivity(MoodActivityDto moodActivityDto)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");


        var moodActivity = new MoodActivity
        {
            MoodId = moodActivityDto.moodId,
            Mood = _repositoryMood.GetById(moodActivityDto.moodId),
            ActivityId = moodActivityDto.activityId,
            Activity = _repositoryActivity.GetById(moodActivityDto.activityId)
        };

        return Created("success", _repositoryMoodActivity.Create(moodActivity));
    }

    [HttpGet("moodactivity")]
    public IActionResult GetAllMoodActivity()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var moodActivity = _repositoryMoodActivity.GetAllMoodActivities(userId);

        return Ok(moodActivity);
    }

    [HttpDelete("delete/{moodId}/{activityId}")]
    public IActionResult DeleteMoodActivity(int moodId, int activityId)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId))
            return Unauthorized("Invalid or expired token.");

        var moodActivity = _repositoryMoodActivity.GetMoodActivityByMoodAndActivityId(moodId, activityId, userId);

        if (moodActivity == null)
            return NotFound("Dara not found.");

        _repositoryMoodActivity.Delete(moodActivity);
        return NoContent();
    }

    [HttpGet("mood/activities/best")]
    public IActionResult GetBestActivities()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

         var averageMood = _repositoryMood.GetAverageMoodValue(userId);
        var activities = _repositoryMoodActivity.GetBestMoodActivities(userId, averageMood);

        if (activities.Count == 0)
        {
            return NotFound();
        }

        return Ok(activities);
    }

    [HttpGet("mood/activities/worst")]
    public IActionResult GetWorstActivities()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var averageMood = _repositoryMood.GetAverageMoodValue(userId);

        var activities = _repositoryMoodActivity.GetWorstMoodActivities(userId,averageMood);


        if (activities.Count == 0)
        {
            return NotFound();
        }

        return Ok(activities);
    }

}
