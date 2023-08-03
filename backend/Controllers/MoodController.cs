
using auth.Helper;
using auth.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;
using Moodie.Helper;
using Dtos.MoodDto;
namespace Moodie.Controllers
{
    
     [Route("api")]
    [ApiController]
    
   public class MoodController:Controller
   {
      private readonly ApplicationDbContext _context;
      private readonly AverageMood _averageMood;
      private readonly IUserRepo _repository;
      private readonly JWTService _jwtService;
      public MoodController(ApplicationDbContext context,AverageMood averageMood,IUserRepo repository,JWTService jwtService)
      {
           _context = context;
           _averageMood = averageMood;
           _repository = repository;
           _jwtService = jwtService;

      }
        
      [HttpPost("add-mood")]
       public IActionResult AddMood( MoodDto moodDto)
       {
           try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = _repository.GetById(userId);
                var mood=new Mood {
                     MoodValue = moodDto.MoodValue,
                     Date = DateTime.Now,
                     User = user,
                     UserId = userId,
                     
                 };
                 _context.Moods.Add(mood);
                 _context.SaveChanges();
                 _averageMood.UpdateAverageMood(_context, userId);
                 return Ok(new { message = "Mood added successfully", mood });
            }catch (Exception e)
            {
                return Unauthorized();
            }
          
       }
    }
}

