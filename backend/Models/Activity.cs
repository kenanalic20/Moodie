using System.ComponentModel.DataAnnotations.Schema;

namespace Moodie.Models;

public class Activity
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string? Description { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
  [ForeignKey("UserId")] public int? UserId { get; set; }

  [ForeignKey("MoodId")] public int? MoodId { get; set; }
  public Mood? Mood { get; set; }
  

}