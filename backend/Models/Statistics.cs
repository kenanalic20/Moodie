using System.ComponentModel.DataAnnotations.Schema;

namespace Moodie.Models;
public class Statistics
{
   public int Id { get; set; }
    public string DayOfWeek { get; set; }
    public double AverageMood { get; set; }
    public int MoodCount { get; set; }
    public DateTime CalculationDate { get; set; } = DateTime.UtcNow;

    [ForeignKey("UserId")]
    public int UserId { get; set; } 
    public User User { get; set; } 
}