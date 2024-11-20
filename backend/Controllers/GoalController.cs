using auth.Helper;
using auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moodie.Data;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("api/goal")]  // Changed from [Route("api")]
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

    [HttpPost]  // Changed from [HttpPost("goal")]
    public IActionResult AddGoal([FromBody] GoalDto goalDto)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);

            var goal = new Goal
            {
                Name = goalDto.Name,
                Description = goalDto.Description,
                GoalType = goalDto.GoalType,
                StartDate = DateTime.Parse(goalDto.StartDate),
                EndDate = DateTime.Parse(goalDto.EndDate),
                Completed = goalDto.Completed,  // Add this line
                UserId = userId
            };

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

    [HttpGet]  // Changed from [HttpGet("goal")]
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

    [HttpDelete("{id:int}")]  // Changed from [HttpDelete("goal/{id:int}")]
    public IActionResult DeleteGoal([FromRoute] int id)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);

            var goal = _repositoryGoal.GetById(id);
            if (goal == null || goal.UserId != userId)
                return NotFound();

            _repositoryGoal.Delete(id);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPut("{id:int}")]  // Changed from [HttpPut("goal/{id:int}")]
    public IActionResult UpdateGoal([FromRoute] int id, [FromBody] GoalDto goalDto)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);

            var goal = _repositoryGoal.GetById(id);
            if (goal == null || goal.UserId != userId)
                return NotFound();

            goal.Name = goalDto.Name;
            goal.Description = goalDto.Description;
            goal.GoalType = goalDto.GoalType;
            goal.StartDate = DateTime.Parse(goalDto.StartDate);
            goal.EndDate = DateTime.Parse(goalDto.EndDate);
            goal.Completed = goalDto.Completed;

            return Ok(_repositoryGoal.Update(goal));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}
