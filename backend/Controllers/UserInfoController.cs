using auth.Helper;
using auth.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;


namespace Moodie.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserInfoController : Controller
    {
        private readonly IUserRepo _repositoryUser;
        private readonly JWTService _jwtService;

        public UserInfoController(IUserRepo repositoryUser,
            JWTService jwtService)
        {
            _repositoryUser = repositoryUser;
            _jwtService = jwtService;
        }
      
        
    }
}