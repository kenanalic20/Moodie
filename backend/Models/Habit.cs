
using System.Text.Json.Serialization;

namespace auth.Models;

public class Habit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CurrentStreak { get; set; }
    public int BestStreak { get; set; }
    public DateTime LastCheckIn { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
}
