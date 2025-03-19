using Microsoft.AspNetCore.Mvc;
using Moodie.Helper;
using Moodie.Interfaces;
using Moodie.Models;
using System.Collections.Generic;
using System.Linq;

namespace Moodie.Controllers;

[Route("api/achievement")]
[ApiController]
public class AchievementController : Controller
{
    private readonly IAchievementRepo _repositoryAchievement;
    private readonly AuthHelper _authHelper;

    public AchievementController(IAchievementRepo repositoryAchievement,
        AuthHelper authHelper)
    {
        _repositoryAchievement = repositoryAchievement;
        _authHelper = authHelper;
    }

    [HttpGet]
    public IActionResult GetUserAchievements()
    {
        if(!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        var userAchievements = _repositoryAchievement.GetUserAchievements(userId);
        
        foreach (var userAchievement in userAchievements)
        {
            if (userAchievement.Achievement == null && userAchievement.AchievementId > 0)
            {
                userAchievement.Achievement = _repositoryAchievement.GetById(userAchievement.AchievementId);
            }
        }
        
        return Ok(userAchievements);
    }
}
