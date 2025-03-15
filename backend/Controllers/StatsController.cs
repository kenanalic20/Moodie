using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class StatsController : Controller
{
    private readonly AuthHelper _authHelper;
    private readonly IMoodRepo _repositoryMood;
    private readonly IStatsRepo _repositoryStats;

    public StatsController(AuthHelper authHelper, IMoodRepo repositoryMood, IStatsRepo repositoryStats)
    {
        _authHelper = authHelper;
        _repositoryMood = repositoryMood;
        _repositoryStats = repositoryStats;
    }

    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var dailyAverageMood = _repositoryMood.GetDailyAverageMood(userId);
        
        var stats= _repositoryStats.GetByUserID(userId);

        return Ok(stats);
    }
    
}
