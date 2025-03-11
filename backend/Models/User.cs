using System.Text.Json.Serialization;

namespace Moodie.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsVerifiedEmail { get; set; }
    public string EmailToken { get; set; }
    public string? EmailTwoStepToken { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpiry { get; set; }
    [JsonIgnore] public string Password { get; set; }
    [JsonIgnore] public List<Mood> Moods { get; set; }
    [JsonIgnore] public List<Notes> Notes { get; set; }
    [JsonIgnore] public List<Habit> Habits { get; set; }
    [JsonIgnore] public List<UserAchievement> Achievements { get; set; }
    [JsonIgnore] public Settings Settings {get; set;}
}
