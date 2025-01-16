using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class SettingsController : Controller
{
    private readonly ISettingsRepo _repositorySettings;
    private readonly IUserRepo _repositoryUser;
    private readonly AuthHelper _authHelper;

    public SettingsController(IUserRepo repositoryUser, ISettingsRepo repositorySettings, AuthHelper authHelper)
    {
        _repositoryUser = repositoryUser;
        _authHelper = authHelper;
        _repositorySettings = repositorySettings;
    }

    [HttpPut("Settings")]
    public IActionResult AddSettings(SettingsDto settingsDto)
    {
        if(_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        var user = _repositoryUser.GetById(userId);
        var settings = new Settings
        {
            DarkMode = settingsDto.DarkMode,
            Language = settingsDto.Language,
            ReducedMotion = settingsDto.ReducedMotion,
            UserId = userId,
            User = user
        };

        return Created("success", _repositorySettings.Create(settings, userId));
    }

    [HttpGet("Settings")]
    public IActionResult GetSettings()
    {
        if(_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        return Ok(_repositorySettings.GetByUserId(userId));
    }

    [HttpDelete("Settings")]
    public IActionResult DeleteSettings()
    {
        if(_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        return Ok(_repositorySettings.Delete(userId));
    }
}