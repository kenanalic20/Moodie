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

    [ForeignKey("ActivityId")]
    public int? ActivityId { get; set; } // Nullable in case no activity is associated
    public Activity Activity { get; set; }
}