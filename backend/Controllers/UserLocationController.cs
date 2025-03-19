using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;
using Moodie.Dtos;
using QuestPDF.Infrastructure;

namespace Moodie.Controllers;

[Route("/api")]
[ApiController]
public class UserLocationController : Controller
{
    private readonly IUserRepo _repositoryUser;
    private readonly AuthHelper _authHelper;
    private readonly IUserLocationRepo _repositoryUserLocation;

    public UserLocationController(
        IUserRepo repositoryUser,
        AuthHelper authHelper,
        IUserLocationRepo repositoryUserLocation
        )
    {
        _repositoryUser = repositoryUser;
        _authHelper = authHelper;
        _repositoryUserLocation = repositoryUserLocation;
    }

    [HttpPost("user-location")]
    public IActionResult AddOrUpdateUserLocation(UserLocationDto userInfoDto)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var userLocation = _repositoryUserLocation.GetByUserId(userId);
        if (userLocation == null)
        {
            userLocation = new UserLocation
            {
                UserId = userId,
                Country = userInfoDto.Country,
                Province = userInfoDto.Province,
                City = userInfoDto.City
            };
            _repositoryUserLocation.Create(userLocation);
        }
        else
        {
            userLocation.Country = userInfoDto.Country;
            userLocation.Province = userInfoDto.Province;
            userLocation.City = userInfoDto.City;
            _repositoryUserLocation.Update(userLocation);
        }

        return Ok(userLocation);
    }

    [HttpGet("user-location")]
    public IActionResult GetUserInfo()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var userLocation = _repositoryUserLocation.GetByUserId(userId);

        return Ok(userLocation);
    }

    [HttpDelete("user-location")]
    public IActionResult DeleteUserInfo()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        _repositoryUserLocation.Delete(userId);
        return NoContent();
    }
}
