using System.Text.Json.Serialization;

namespace Moodie.Models;
public class MoodActivity
{
    public int MoodId { get; set; }
    [JsonIgnore]
    public Mood? Mood { get; set; }

    public int ActivityId { get; set; }
    [JsonIgnore]
    public Activity? Activity { get; set; }
}