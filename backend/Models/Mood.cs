using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Moodie.Models;

public class Mood
{    
    public int Id { get; set; }
    public int MoodValue { get; set; }
    public DateTime Date { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    public ICollection<MoodActivity> MoodActivities { get; set; } = new List<MoodActivity>();
    public ICollection<Notes> Notes { get; set; } = new List<Notes>();

}
