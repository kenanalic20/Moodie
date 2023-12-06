using auth.Helper;
using auth.Models;
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

    public UserInfoController(IUserRepo repositoryUser,
        JWTService jwtService, IUserInfoRepo repositoryUserInfo)
    {
        _repositoryUser = repositoryUser;
        _jwtService = jwtService;
        _repositoryUserInfo = repositoryUserInfo;
    }

    [HttpPut("user-info")]
    public IActionResult AddUserInfo(UserInfoDto uidto)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
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
        catch (Exception e)
        {
            return Unauthorized("Invalid or expired token.");
        }
    }

    [HttpGet("user-info")]
    public IActionResult GetUserInfo()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
            return Ok(_repositoryUserInfo.GetByUserId(userId));
        }
        catch (Exception e)
        {
            return Unauthorized("Invalid or expired token.");
        }
    }

    [HttpDelete("user-info")]
    public IActionResult DeleteUserInfo()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
            _repositoryUserInfo.Delete(userId);
            return Ok("success");
        }
        catch (Exception e)
        {
            return Unauthorized("Invalid or expired token.");
        }
    }
}