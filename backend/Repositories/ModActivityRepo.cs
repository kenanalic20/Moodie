using Moodie.Models;
using Moodie.Interfaces;
using Moodie.Data;
using Microsoft.EntityFrameworkCore;

namespace Moodie.Repositories;
public class ModActivityRepo : IMoodActivityRepo
{
    private readonly ApplicationDbContext _context;
    public ModActivityRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public MoodActivity Create(MoodActivity moodActivity)
    {
        _context.MoodActivities.Add(moodActivity);
        _context.SaveChanges();
        return moodActivity;
    }

    public List<MoodActivity> GetAllMoodActivities(int userId)
    {
        return _context.MoodActivities
        .Include(ma => ma.Mood)
        .Include(ma => ma.Activity)
        .Where(ma => ma.Mood.UserId == userId && ma.Activity.UserId == userId)
        .ToList();
    }

    public MoodActivity? GetMoodActivityByMoodAndActivityId(int moodId, int activityId, int userId)
    {
        return _context.MoodActivities
            .Include(ma => ma.Mood)
            .Include(ma => ma.Activity)
            .FirstOrDefault(ma => ma.MoodId == moodId && ma.ActivityId == activityId && ma.Mood.UserId == userId);
    }

    public void Delete(MoodActivity moodActivity)
    {
        _context.MoodActivities.Remove(moodActivity);
        _context.SaveChanges();
    }

    public List<Activity> GetBestMoodActivities(int userId, double average)
    {
        var moodActivities = _context.MoodActivities
            .Include(ma => ma.Mood)
            .Include(ma => ma.Activity)
            .Where(ma => ma.Mood.UserId == userId)
            .Select(ma => new
            {
                Activity = ma.Activity,
                MoodValue = ma.Mood.MoodValue
            })
            .ToList();

        var bestActivities = moodActivities
            .GroupBy(ma => ma.Activity)
            .Select(g => new
            {
                Activity = g.Key,
                AverageMoodValue = g.Average(ma => ma.MoodValue)
            })
            .Where(g=>g.AverageMoodValue >= average)
            .OrderByDescending(a => a.AverageMoodValue)
            .Take(5)
            .Select(a => a.Activity)
            .ToList();

        return bestActivities;
    }

    public List<Activity> GetWorstMoodActivities(int userId, double average)
    {
        var moodActivities = _context.MoodActivities
            .Include(ma => ma.Mood)
            .Include(ma => ma.Activity)
            .Where(ma => ma.Mood.UserId == userId)
            .Select(ma => new
            {
                Activity = ma.Activity,
                MoodValue = ma.Mood.MoodValue
            })
            .ToList(); 

        var worstActivities = moodActivities
            .GroupBy(ma => ma.Activity)
            .Select(g => new
            {
                Activity = g.Key,
                AverageMoodValue = g.Average(ma => ma.MoodValue)
            })
            .Where(g=>g.AverageMoodValue < average)
            .OrderBy(a => a.AverageMoodValue)
            .Take(5)
            .Select(a => a.Activity)
            .ToList();

        return worstActivities;
    }

    public List<Activity> GetActivitiesByMoodId(int moodId, int userId){
        return _context.MoodActivities
        .Include(ma => ma.Mood)
        .Include(ma => ma.Activity)
        .Where(ma => ma.MoodId == moodId && ma.Mood.UserId == userId)
        .Select(ma => ma.Activity)
        .ToList();
    }


}