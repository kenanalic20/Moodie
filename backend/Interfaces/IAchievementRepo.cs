using Moodie.Models;

namespace Moodie.Interfaces;

public interface IAchievementRepo
{
    Achievement GetBySlug(string slug);
    Achievement GetById(int id);
    bool HasUserEarnedAchievement(int userId, string achievementSlug);
    UserAchievement AddUserAchievement(int userId, string achievementSlug);
    List<UserAchievement> GetUserAchievements(int userId);
}
