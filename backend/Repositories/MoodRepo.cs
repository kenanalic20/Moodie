using Moodie.Models;
using Moodie.Interfaces;
using Moodie.Data;
using Moodie.Dtos;



namespace Moodie.Repositories;

public class MoodRepo : IMoodRepo
{
    private readonly ApplicationDbContext _context;

    public MoodRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public double GetAverageMoodValue(int userId)
    {
        return _context.Moods.Where(u => u.UserId == userId).Select(m=>m.MoodValue).Average();
    }

    public Mood Create(Mood mood)
    {
        _context.Moods.Add(mood);
        _context.SaveChanges();
        
        return mood;
    }

    public Mood GetById(int? id)
    {
        if (id == null) return null;

        return _context.Moods.Find(id);
    }

    public List<Mood> GetByUserId(int userId)
    {
        return _context.Moods.Where(u => u.UserId == userId).ToList();
    }

    public List<MoodExportDto> GetExportByUserId(int userId)
    {
        return _context.Moods.Where(u => u.UserId == userId).Select(u=>new MoodExportDto{Id=u.Id,Mood=u.MoodValue,Date=u.Date}).ToList();
    }
    
}
