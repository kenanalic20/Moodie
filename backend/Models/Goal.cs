using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Moodie.Models;

public class Goal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string GoalType { get; set; }
    public bool Completed { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
}
