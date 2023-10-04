using System.Net.Mime;
using auth.Helper;
using auth.Models;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moodie.Data;
using Moodie.Dtos;


namespace Moodie.Controllers
{
    [Route("api")]
    [ApiController]
    public class NotesController : Controller
    {
        

        private readonly IUserRepo _repositoryUser;
        private readonly INotesRepo _repositoryNotes;
        private readonly JWTService _jwtService;

        public NotesController( IUserRepo repositoryUser,
            JWTService jwtService, INotesRepo repositoryNotes)
        {
           
            _repositoryUser = repositoryUser;
            _repositoryNotes = repositoryNotes;
            _jwtService = jwtService;
        }

        [HttpPost("add-notes")]
        public IActionResult AddNotes([FromForm] NotesDto notesDto)
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = _repositoryUser.GetById(userId);
                byte[] imageData = null;
                if (notesDto.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        notesDto.Image.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();
                    }
                }
                    var notes = new Notes
                    {
                        Title = notesDto.Title,
                        Image =imageData,
                        Description = notesDto.Description,
                        Date = DateTime.Now,
                        UserId = userId,
                        User = user
                    };
                  return Created("success", _repositoryNotes.Create(notes));
                
                 
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

        [HttpGet("get-notes")]
        public IActionResult GetNotes()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
             
                var notes = _repositoryNotes.GetByUserId(userId);
                
                return Ok(notes );
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
}