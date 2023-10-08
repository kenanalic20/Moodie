using auth.Helper;
using auth.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;
using Moodie.Dtos;


namespace Moodie.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserInfoController : Controller
    {
        private readonly IUserInfoRepo _repositoryUserInfo;
        private readonly IUserRepo _repositoryUser;
        private readonly JWTService _jwtService;

        public UserInfoController(IUserRepo repositoryUser,
            JWTService jwtService, IUserInfoRepo repositoryUserInfo)
        {
            _repositoryUser = repositoryUser;
            _jwtService = jwtService;
            _repositoryUserInfo = repositoryUserInfo;
        }

        [HttpPut("add-userInfo")]
        public IActionResult AddUserInfo(UserInfoDto uidto)
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
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

        [HttpGet("get-userInfo")]
        public IActionResult GetUserInfo()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                return Ok(_repositoryUserInfo.GetByUserId(userId));
            }
            catch (Exception e)
            {
                return Unauthorized("Invalid or expired token.");

            }
        }

        [HttpDelete("delete-userInfo")]
        public IActionResult DeleteUserInfo()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                _repositoryUserInfo.Delete(userId);
                return Ok("success");
            }
            catch (Exception e)
            {
                return Unauthorized("Invalid or expired token.");

            }
        }
    }
}