using auth.Dtos;
using auth.Helper;
using auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Moodie.Data;
using Moodie.Helper;
namespace Moodie.Controllers
{
    
     [Route("api")]
    [ApiController]
   public class MoodController:Controller
   {
      private readonly ApplicationDbContext _context;
      private readonly AverageMood _averageMood;
      public MoodController(ApplicationDbContext context,AverageMood averageMood)
      {
           _context = context;
           _averageMood = averageMood;
       }
      [HttpPost("add-mood")]
       public IActionResult AddMood(Mood mood)
       {
           mood.Date = DateTime.UtcNow; // Set the mood's date to the current UTC time
           mood.UserId = int.Parse(User.Identity.Name); // Get the UserId from the authenticated user's claim
            _context.Moods.Add(mood);
            _context.SaveChanges();
           _averageMood.UpdateAverageMood(_context, mood.UserId);
           return Ok(new { message = "Mood added successfully", mood });
       }
    }
}

