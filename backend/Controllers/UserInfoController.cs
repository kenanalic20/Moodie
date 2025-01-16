using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("/api")]
[ApiController]
public class UserInfoController : Controller
{
    private readonly JWTService _jwtService;
    private readonly IUserRepo _repositoryUser;
    private readonly IUserInfoRepo _repositoryUserInfo;
    private readonly AuthHelper _authHelper;

    public UserInfoController(IUserRepo repositoryUser,
        AuthHelper authHelper, IUserInfoRepo repositoryUserInfo)
    {
        _repositoryUser = repositoryUser;
        _authHelper = authHelper;
        _repositoryUserInfo = repositoryUserInfo;
    }

    [HttpPut("user-info")]
    public IActionResult AddUserInfo(UserInfoDto uidto)
    {
        if(_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
       
        var user = _repositoryUser.GetById(userId);
        var userInfo = new UserInfo
        {
            FirstName = uidto.FirstName,
            LastName = uidto.LastName,
            Gender = uidto.Gender,
            Birthday = uidto.Birthday,
            UserId = userId,
            User = user
        };

        return Created("success", _repositoryUserInfo.Create(userInfo, userId));
    }

    [HttpGet("user-info")]
    public IActionResult GetUserInfo()
    {
        if(_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        return Ok(_repositoryUserInfo.GetByUserId(userId));
    }

    [HttpDelete("user-info")]
    public IActionResult DeleteUserInfo()
    {
        if(_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        _repositoryUserInfo.Delete(userId);
        return Ok("success");
    }
}