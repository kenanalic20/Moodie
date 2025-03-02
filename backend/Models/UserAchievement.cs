using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Moodie.Models;

public class UserAchievement
{
    public int Id { get; set; }
    public DateTime DateEarned { get; set; }
    
    [ForeignKey("UserId")] public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
    
    [ForeignKey("AchievementId")] public int AchievementId { get; set; }
    public Achievement Achievement { get; set; }
}
