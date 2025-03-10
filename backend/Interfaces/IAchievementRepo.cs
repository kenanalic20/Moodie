using Moodie.Models;

namespace Moodie.Interfaces;

public interface IAchievementRepo
{
    Achievement GetBySlug(string slug);
    bool HasUserEarnedAchievement(int userId, string achievementSlug);
    UserAchievement AddUserAchievement(int userId, string achievementSlug);
}
