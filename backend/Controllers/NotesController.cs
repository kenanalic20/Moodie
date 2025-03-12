using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Dtos;
using Moodie.Interfaces;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class NotesController : Controller
{
    private readonly INotesRepo _repositoryNotes;
    private readonly IUserRepo _repositoryUser;
    private readonly AuthHelper _authHelper;
    private readonly IAchievementRepo _achievementRepo;
    private readonly ImageHelper _imageHelper;

    public NotesController(
        IUserRepo repositoryUser,
        AuthHelper authHelper,
        INotesRepo repositoryNotes,
        IAchievementRepo achievementRepo,
        ImageHelper imageHelper)
    {
        _repositoryUser = repositoryUser;
        _repositoryNotes = repositoryNotes;
        _authHelper = authHelper;
        _achievementRepo = achievementRepo;
        _imageHelper = imageHelper;
    }

    [HttpPost("notes")]
    public IActionResult AddNotes([FromForm]NotesDto notesDto)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var user = _repositoryUser.GetById(userId);

        if (string.IsNullOrEmpty(notesDto.Title))
        {
            return BadRequest("Title is required");
        }

        if (string.IsNullOrEmpty(notesDto.Description))
        {
            return BadRequest("Description is required");
        }

        string imagePath = _imageHelper.SaveImage(notesDto.Image);

        var notes = new Notes
        {
            Title = notesDto.Title,
            ImagePath = imagePath, // Store the file path
            Description = notesDto.Description,
            Date = DateTime.Now,
            UserId = userId,
            User = user,
            MoodId = notesDto.MoodId
        };

        // Log the note object before saving to confirm MoodId is set
        Console.WriteLine($"Creating note with MoodId: {notes.MoodId}");

        var createdNote = _repositoryNotes.Create(notes);

        // Check for achievements
        var newAchievements = CheckAndAwardNotesAchievements(userId);

        var result = new
        {
            note = createdNote,
            newAchievements = newAchievements
        };

        return Created("success", result);
    }

    [HttpGet("notes")]
    public IActionResult GetNotes()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var notes = _repositoryNotes.GetByUserId(userId);

        return Ok(notes);
    }

    [HttpDelete("notes/{id}")]
    public IActionResult DeleteNotes(int id)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        _repositoryNotes.Delete(id);
        
        return Ok("Notes deleted successfully");
    }

    private List<UserAchievement> CheckAndAwardNotesAchievements(int userId)
    {
        var newAchievements = new List<UserAchievement>();

        // Check for first note achievement
        if (!_achievementRepo.HasUserEarnedAchievement(userId, "added_note"))
        {
            var achievement = _achievementRepo.AddUserAchievement(userId, "added_note");
            if (achievement != null) newAchievements.Add(achievement);
        }

        return newAchievements;
    }
}
