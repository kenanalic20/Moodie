using auth.Helper;
using auth.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;
using Moodie.Dtos;

namespace Moodie.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserImageController: Controller
    {
        private readonly IUserRepo _repositoryUser;
        private readonly JWTService _jwtService;
        private readonly IUserImageRepo _repositoryUserImage;
        private readonly IUserInfoRepo _repositoryUserInfo;
       public UserImageController(IUserRepo repositoryUser, JWTService jwtService,
            IUserImageRepo repositoryUserImage, IUserInfoRepo repositoryUserInfo)
        {
            _repositoryUser = repositoryUser;
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
                int userId = int.Parse(token.Issuer);
                var user = _repositoryUser.GetById(userId);
                var userInfo= _repositoryUserInfo.GetByUserId(userId);
                byte[] imageData = null;
                if (userImageDto.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        userImageDto.Image.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();
                    }
                }
                var userImage = new UserImage
                {
                    Status = userImageDto.Status,
                    Image = imageData,
                    Date = DateTime.Now,
                    UserInfoId = userInfo.Id,
                };
                return Created("success", _repositoryUserImage.Create(userImage));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}