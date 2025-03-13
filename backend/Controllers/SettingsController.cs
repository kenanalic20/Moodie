using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class SettingsController : Controller
{
    private readonly ISettingsRepo _repositorySettings;
    private readonly AuthHelper _authHelper;
    private readonly IUserRepo _repositoryUser;

    public SettingsController(ISettingsRepo repositorySettings, AuthHelper authHelper, IUserRepo repositoryUser)
    {
        _authHelper = authHelper;
        _repositorySettings = repositorySettings;
        _repositoryUser = repositoryUser;
    }

    [HttpPut("settings")]
    public IActionResult AddSettings(SettingsDto settingsDto)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        var existingSettings = _repositorySettings.GetByUserId(userId);

        if (existingSettings != null)
        {
            existingSettings.DarkMode = settingsDto.DarkMode ?? existingSettings.DarkMode;
            existingSettings.LanguageId = settingsDto.LanguageId ?? existingSettings.LanguageId;
            existingSettings.TwoFactorEnabled = settingsDto.TwoFactorEnabled ?? existingSettings.TwoFactorEnabled;

            _repositorySettings.Update(existingSettings, userId);
            return Ok(new {message = "Settings updated successfully"});
        }
        else
        {
            var newSettings = new Settings
            {
                DarkMode = settingsDto.DarkMode ?? false,
                LanguageId = settingsDto.LanguageId ?? 1,
                TwoFactorEnabled = settingsDto.TwoFactorEnabled ?? false,
                UserId = userId,
                User = _repositoryUser.GetById(userId)
            };

            _repositorySettings.Create(newSettings, null);
            return Created("success", newSettings);
        }
    }

    [HttpGet("settings")]
    public IActionResult GetSettings()
    {
        if(!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
   
        var settings = _repositorySettings.GetByUserId(userId);

        if(settings==null) {
            return NotFound("No settings found.");
        }

        return Ok(settings);
    }
}
