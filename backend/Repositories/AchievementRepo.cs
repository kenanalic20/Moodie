using Moodie.Interfaces;
using Moodie.Models;
using Moodie.Data;

namespace Moodie.Repositories;

public class AchievementRepo : IAchievementRepo
{
    private readonly ApplicationDbContext _context;

    public AchievementRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public Achievement GetBySlug(string slug)
    {
        return _context.Achievements.FirstOrDefault(a => a.Slug == slug);
    }

    public Achievement GetById(int id)
    {
        return _context.Achievements.FirstOrDefault(a => a.Id == id);
    }

    public bool HasUserEarnedAchievement(int userId, string achievementSlug)
    {
        var achievement = GetBySlug(achievementSlug);
        if (achievement == null) return false;

        return _context.UserAchievements
            .Any(ua => ua.UserId == userId && ua.AchievementId == achievement.Id);
    }

    public UserAchievement AddUserAchievement(int userId, string achievementSlug)
    {
        var achievement = GetBySlug(achievementSlug);
        if (achievement == null) return null;

        var userAchievement = new UserAchievement
        {
            UserId = userId,
            AchievementId = achievement.Id,
            DateEarned = DateTime.Now
        };

        _context.UserAchievements.Add(userAchievement);
        _context.SaveChanges();

        userAchievement.Achievement = achievement;
        return userAchievement;
    }

    public List<UserAchievement> GetUserAchievements(int userId)
    {
        return _context.UserAchievements
            .Where(ua => ua.UserId == userId)
            .ToList();
    }
}
