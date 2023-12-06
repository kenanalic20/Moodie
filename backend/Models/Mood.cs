using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace auth.Models;

public class Mood
{
    public int Id { get; set; }
    public double MoodValue { get; set; }

    public DateTime Date { get; set; }

    [ForeignKey("UserId")] public int UserId { get; set; }

    [JsonIgnore] public User User { get; set; }
}