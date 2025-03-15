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
        return _context.Moods.Where(u => u.UserId == userId).Select(m => m.MoodValue).Average();
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
        return _context.Moods.Where(u => u.UserId == userId).Select(u => new MoodExportDto { Id = u.Id, Mood = u.MoodValue, Date = u.Date }).ToList();
    }

    public void Delete(int id)
    {
        var mood = _context.Moods.Find(id);
        _context.Moods.Remove(mood);
        _context.SaveChanges();
    }

    public Dictionary<string, double> GetDailyAverageMood(int userId)
    {
        var moodAverages = _context.Moods
            .Where(u => u.UserId == userId)
            .AsEnumerable()
            .GroupBy(m => m.Date.DayOfWeek.ToString().Substring(0, 3))
            .Select(g => new
            {
                DayOfWeek = g.Key,
                AverageMood = g.Average(m => m.MoodValue),
                MoodCount = g.Count()
            })
            .ToDictionary(x => x.DayOfWeek, x => new { x.AverageMood, x.MoodCount });

        var daysOfWeek = Enum.GetNames(typeof(DayOfWeek))
            .Select(day => day.Substring(0, 3))
            .ToList();

        foreach (var day in daysOfWeek)
        {
            if (!moodAverages.ContainsKey(day))
            {
                moodAverages[day] = new { AverageMood = 0.0, MoodCount = 0 };
            }
        }

        foreach (var entry in moodAverages)
        {
            var existingStat = _context.Statistics
                .FirstOrDefault(s => s.UserId == userId && s.DayOfWeek == entry.Key);

            if (existingStat == null)
            {
                var dailyStat = new Statistics
                {
                    UserId = userId,
                    DayOfWeek = entry.Key,
                    AverageMood = entry.Value.AverageMood,
                    MoodCount = entry.Value.MoodCount,
                    CalculationDate = DateTime.UtcNow
                };

                _context.Statistics.Add(dailyStat);
            }
            else
            {
                existingStat.AverageMood = entry.Value.AverageMood;
                existingStat.MoodCount = entry.Value.MoodCount;
                existingStat.CalculationDate = DateTime.UtcNow;
            }
        }

        _context.SaveChanges();

        return moodAverages.ToDictionary(x => x.Key, x => x.Value.AverageMood);
    }

}
