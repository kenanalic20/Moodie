using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class UserImageController : Controller
{
    private readonly IUserImageRepo _repositoryUserImage;
    private readonly IUserInfoRepo _repositoryUserInfo;
    private readonly AuthHelper _authHelper;
    private readonly ImageHelper _imageHelper;

    public UserImageController(
        IUserImageRepo repositoryUserImage,
        IUserInfoRepo repositoryUserInfo,
        AuthHelper authHelper,
        ImageHelper imageHelper)
    {
        _repositoryUserImage = repositoryUserImage;
        _repositoryUserInfo = repositoryUserInfo;
        _authHelper = authHelper;
        _imageHelper = imageHelper;
    }


    [HttpGet("user-image")]
    public IActionResult GetUserImage()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var userImage = _repositoryUserImage.GetByUserId(userId);

        if (userImage == null)
        {
            return NotFound();
        }

        return Ok(userImage);
    }

    [HttpPost("user-image")]
    public IActionResult AddOrUpdateUserImage([FromForm] UserImageDto userImageDto)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var existingUserImage = _repositoryUserImage.GetByUserId(userId);

        if (existingUserImage != null)
        {
            existingUserImage.ImagePath = _imageHelper.UpdateImage(userImageDto.Image, existingUserImage.ImagePath);
            existingUserImage.Status = userImageDto.Status;
            existingUserImage.Date = DateTime.Now;

            var updatedUserImage = _repositoryUserImage.Update(existingUserImage);
            return Ok(updatedUserImage);
        }
        else
        {
            var userImage = new UserImage
            {
                ImagePath = _imageHelper.SaveImage(userImageDto.Image),
                Status = userImageDto.Status,
                Date = DateTime.Now,
                UserId = userId
            };

            return Created("Success", _repositoryUserImage.Create(userImage));
        }

    }

    [HttpDelete("user-image")]
    public IActionResult DeleteUserImage()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        _repositoryUserImage.Delete(userId);

        return NoContent();
    }
}
