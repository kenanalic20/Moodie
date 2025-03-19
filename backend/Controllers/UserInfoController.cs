using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("/api")]
[ApiController]
public class UserInfoController : Controller
{
    private readonly IUserInfoRepo _repositoryUserInfo;
    private readonly AuthHelper _authHelper;

    public UserInfoController(
        AuthHelper authHelper,
        IUserInfoRepo repositoryUserInfo
        )
    {
        _authHelper = authHelper;
        _repositoryUserInfo = repositoryUserInfo;
    }

    [HttpPost("user-info")]
    public IActionResult AddOrUpdateUserInfo(UserInfoDto userInfoDto)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var userInfo = _repositoryUserInfo.GetByUserId(userId);
        if (userInfo == null)
        {
            userInfo = new UserInfo
            {
                UserId = userId,
                FirstName = userInfoDto.FirstName,
                LastName = userInfoDto.LastName,
                Gender = userInfoDto.Gender,
                Birthday = userInfoDto.Birthday,

            };
            _repositoryUserInfo.Create(userInfo);
        }
        else
        {
            userInfo.FirstName = userInfoDto.FirstName;
            userInfo.LastName = userInfoDto.LastName;
            userInfo.Gender = userInfoDto.Gender;
            userInfo.Birthday = userInfoDto.Birthday;
            _repositoryUserInfo.Update(userInfo);
        }

        return Ok(userInfo);
    }

    [HttpGet("user-info")]
    public IActionResult GetUserInfo()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var userInfo = _repositoryUserInfo.GetByUserId(userId);

        return Ok(userInfo);
    }

    [HttpDelete("user-info")]
    public IActionResult DeleteUserInfo()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        _repositoryUserInfo.Delete(userId);
        return NoContent();
    }
}
