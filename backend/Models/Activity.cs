using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Moodie.Models;

public class Activity
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string? Description { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  [ForeignKey("UserId")]
  public int? UserId { get; set; }
  [JsonIgnore]
  public ICollection<MoodActivity> MoodActivities { get; set; } = new List<MoodActivity>();

}