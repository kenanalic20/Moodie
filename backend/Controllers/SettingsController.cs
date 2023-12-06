using auth.Helper;
using auth.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class SettingsController : Controller
{
    private readonly JWTService _jwtService;
    private readonly ISettingsRepo _repositorySettings;
    private readonly IUserRepo _repositoryUser;

    public SettingsController(IUserRepo repositoryUser, JWTService jwtService, ISettingsRepo repositorySettings)
    {
        _repositoryUser = repositoryUser;
        _jwtService = jwtService;
        _repositorySettings = repositorySettings;
    }

    [HttpPut("Settings")]
    public IActionResult AddSettings(SettingsDto settingsDto)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("Settings")]
    public IActionResult GetSettings()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
            return Ok(_repositorySettings.GetByUserId(userId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete("Settings")]
    public IActionResult DeleteSettings()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
            return Ok(_repositorySettings.Delete(userId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}