using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class UserImageController : Controller
{
    private readonly IUserImageRepo _repositoryUserImage;
    private readonly IUserInfoRepo _repositoryUserInfo;
    private readonly AuthHelper _authHelper;

    public UserImageController(
        IUserImageRepo repositoryUserImage, IUserInfoRepo repositoryUserInfo, AuthHelper authHelper)
    {
        _repositoryUserImage = repositoryUserImage;
        _repositoryUserInfo = repositoryUserInfo;
        _authHelper = authHelper;
    }

    [HttpPut("UserImage")]
    public IActionResult AddUserImage([FromForm] UserImageDto userImageDto)
    {
       if(_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var userInfo = _repositoryUserInfo.GetByUserId(userId);
        byte[] imageData = null;
        if (userImageDto.Image != null)
            using (var memoryStream = new MemoryStream())
            {
                userImageDto.Image.CopyTo(memoryStream);
                imageData = memoryStream.ToArray();
            }

        var userImage = new UserImage
        {
            Status = userImageDto.Status,
            Image = imageData,
            Date = DateTime.Now,
            UserInfoId = userInfo.Id
        };

        return Created("success", _repositoryUserImage.Create(userImage, userInfo.Id));
    }

    [HttpGet("UserImage")]
    public IActionResult GetUserImage()
    {
        if(_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var userInfo = _repositoryUserInfo.GetByUserId(userId);

        return Ok(_repositoryUserImage.GetByUserInfoId(userInfo.Id));
    }

    [HttpDelete("UserImage")]
    public IActionResult DeleteUserImage()
    {
        if(_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        var userInfo = _repositoryUserInfo.GetByUserId(userId);

        _repositoryUserImage.Delete(userInfo.Id);

        return Ok("success");
    }
}