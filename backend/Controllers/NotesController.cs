using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moodie.Data;
using Moodie.Dtos;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class NotesController : Controller
{
    private readonly INotesRepo _repositoryNotes;
    private readonly IUserRepo _repositoryUser;
    private readonly AuthHelper _authHelper;

    public NotesController(IUserRepo repositoryUser,
        AuthHelper authHelper, INotesRepo repositoryNotes)
    {
        _repositoryUser = repositoryUser;
        _repositoryNotes = repositoryNotes;
        _authHelper = authHelper;
    }

    [HttpPost("notes")]
    public IActionResult AddNotes([FromForm] NotesDto notesDto)
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        if (userId == 0) return Unauthorized("Invalid or expired token.");
        
        var user = _repositoryUser.GetById(userId);
        byte[] imageData = null;
        if (notesDto.Image != null)
            using (var memoryStream = new MemoryStream())
            {
                notesDto.Image.CopyTo(memoryStream);
                imageData = memoryStream.ToArray();
            }

        var notes = new Notes
        {
            Title = notesDto.Title,
            Image = imageData,
            Description = notesDto.Description,
            Date = DateTime.Now,
            UserId = userId,
            User = user
        };
        return Created("success", _repositoryNotes.Create(notes));
    }

    [HttpGet("notes")]
    public IActionResult GetNotes()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        if (userId == 0) return Unauthorized("Invalid or expired token.");

        var notes = _repositoryNotes.GetByUserId(userId);

        return Ok(notes); 
    }

    //Trebat ce popravit brise notes jedan po jedan za sad
    [HttpDelete("notes")]
    public IActionResult DeleteNotes()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        if (userId == 0) return Unauthorized("Invalid or expired token.");
       
        _repositoryNotes.Delete(userId);
        return Ok(); 
    }
}