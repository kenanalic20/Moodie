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
    private readonly JWTService _jwtService;
    private readonly IUserImageRepo _repositoryUserImage;
    private readonly IUserInfoRepo _repositoryUserInfo;

    public UserImageController(JWTService jwtService,
        IUserImageRepo repositoryUserImage, IUserInfoRepo repositoryUserInfo)
    {
        _jwtService = jwtService;
        _repositoryUserImage = repositoryUserImage;
        _repositoryUserInfo = repositoryUserInfo;
    }

    [HttpPut("UserImage")]
    public IActionResult AddUserImage([FromForm] UserImageDto userImageDto)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);

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
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("UserImage")]
    public IActionResult GetUserImage()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
            var userInfo = _repositoryUserInfo.GetByUserId(userId);
            return Ok(_repositoryUserImage.GetByUserInfoId(userInfo.Id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("UserImage")]
    public IActionResult DeleteUserImage()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
            var userInfo = _repositoryUserInfo.GetByUserId(userId);
            _repositoryUserImage.Delete(userInfo.Id);
            return Ok("success");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}