using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Dtos;
using Moodie.Interfaces;

namespace Moodie.Controllers;

[Route("api/goal")]  // Changed from [Route("api")]
[ApiController]
public class GoalController : Controller
{
    private readonly IGoalRepo _repositoryGoal;
    private readonly IUserRepo _repositoryUser;
    private readonly AuthHelper _authHelper;

    public GoalController(IUserRepo repositoryUser,
        AuthHelper authHelper, IGoalRepo repositoryGoal)
    {
        _repositoryUser = repositoryUser;
        _repositoryGoal = repositoryGoal;
        _authHelper = authHelper;
    }

    [HttpPost]  // Changed from [HttpPost("goal")]
    public IActionResult AddGoal([FromBody] GoalDto goalDto)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
       
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

    [HttpGet]  // Changed from [HttpGet("goal")]
    public IActionResult GetGoal()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
    
        var Goal = _repositoryGoal.GetByUserId(userId);

        return Ok(Goal); 
    }

    [HttpDelete("{id:int}")]  // Changed from [HttpDelete("goal/{id:int}")]
    public IActionResult DeleteGoal([FromRoute] int id)
    {
       
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var goal = _repositoryGoal.GetById(id);

        if (goal == null || goal.UserId != userId) return NotFound();

        _repositoryGoal.Delete(id);

        return Ok();
       
    }

    [HttpPut("{id:int}")]  // Changed from [HttpPut("goal/{id:int}")]
    public IActionResult UpdateGoal([FromRoute] int id, [FromBody] GoalDto goalDto)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var goal = _repositoryGoal.GetById(id);

        if (goal == null || goal.UserId != userId) return NotFound();

        goal.Name = goalDto.Name;
        goal.Description = goalDto.Description;
        goal.GoalType = goalDto.GoalType;
        goal.StartDate = DateTime.Parse(goalDto.StartDate);
        goal.EndDate = DateTime.Parse(goalDto.EndDate);
        goal.Completed = goalDto.Completed;

        return Ok(_repositoryGoal.Update(goal));
    }
}
