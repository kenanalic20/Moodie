using Newtonsoft.Json;

namespace auth.Models;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public ICollection<Mood> Moods { get; set; } // Collection of moods associated with this activity
}