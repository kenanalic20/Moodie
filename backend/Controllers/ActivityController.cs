using auth.Helper;
using auth.Models;
using Dtos.MoodDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moodie.Data;


namespace Moodie.Controllers;
[Route("api")]
[ApiController]
public class ActivityController:Controller{
    private readonly ApplicationDbContext _context;
    private readonly JWTService _jwtService;
    private readonly IMoodRepo _repositoryMood;
    private readonly IUserRepo _repositoryUser;
    public ActivityController(ApplicationDbContext context, IUserRepo repositoryUser,
        JWTService jwtService, IMoodRepo repositoryMood){
            _context=context;
            _jwtService=jwtService;
            _repositoryMood=repositoryMood;
            _repositoryUser=repositoryUser;
        }

[HttpGet("mood/activities")]
    public IActionResult GetActivities()
    {
        try
        {
            //get users moods from db based on userid
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);

            var activities = _repositoryMood.GetAllActivities();
            return Ok(activities);
        }
        catch (SecurityTokenException ex) // Catch the specific exception type
        {
            return Unauthorized("Invalid or expired token.");
        }
        catch (Exception e) // Catch any other unexpected exception
        {
            return StatusCode(500, "An error occurred.");
        }
    }
}
