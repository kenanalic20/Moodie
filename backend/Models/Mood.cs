using System.ComponentModel.DataAnnotations.Schema;

namespace Moodie.Models;

public class Mood
{
    public int Id { get; set; }
    public double MoodValue { get; set; }
    public DateTime Date { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
}