using auth.Helper;
using auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moodie.Data;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class GoalController : Controller
{
    private readonly JWTService _jwtService;
    private readonly IGoalRepo _repositoryGoal;
    private readonly IUserRepo _repositoryUser;

    public GoalController(IUserRepo repositoryUser,
        JWTService jwtService, IGoalRepo repositoryGoal)
    {
        _repositoryUser = repositoryUser;
        _repositoryGoal = repositoryGoal;
        _jwtService = jwtService;
    }

    [HttpPost("goal")]
    public IActionResult AddGoal([FromForm] GoalDto GoalDto)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
            var user = _repositoryUser.GetById(userId);
            byte[] imageData = null;
            // TODO
            var goal = new Goal();
            return Created("success", _repositoryGoal.Create(goal));
        }
        catch (SecurityTokenException ex)
        {
            return Unauthorized("Invalid or expired token.");
        }
        catch (Exception e) // Catch any other unexpected exception
        {
            return StatusCode(500, "An error occurred.");
        }
    }

    [HttpGet("goal")]
    public IActionResult GetGoal()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);

            var Goal = _repositoryGoal.GetByUserId(userId);

            return Ok(Goal);
        }
        catch (SecurityTokenException ex)
        {
            return Unauthorized("Invalid or expired token.");
        }
        catch (Exception e) // Catch any other unexpected exception
        {
            return StatusCode(500, "An error occurred.");
        }
    }
}