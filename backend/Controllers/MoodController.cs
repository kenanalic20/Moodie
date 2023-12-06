﻿using auth.Helper;
using auth.Models;
using Dtos.MoodDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moodie.Data;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class MoodController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly JWTService _jwtService;
    private readonly IMoodRepo _repositoryMood;

    private readonly IUserRepo _repositoryUser;

    public MoodController(ApplicationDbContext context, IUserRepo repositoryUser,
        JWTService jwtService, IMoodRepo repositoryMood)
    {
        _context = context;
        _repositoryUser = repositoryUser;
        _repositoryMood = repositoryMood;
        _jwtService = jwtService;
    }

    [HttpPost("mood")]
    public IActionResult AddMood(MoodDto moodDto)
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
            var user = _repositoryUser.GetById(userId);
            var mood = new Mood
            {
                MoodValue = moodDto.MoodValue,
                Date = DateTime.Now,
                User = user,
                UserId = userId
            };
            return Created("success", _repositoryMood.Create(mood));
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

    [HttpGet("mood")]
    public IActionResult GetMoods()
    {
        try
        {
            //get users moods from db based on userid
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);

            var moods = _repositoryMood.GetByUserId(userId);
            return Ok(moods);
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