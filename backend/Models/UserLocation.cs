using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Moodie.Models;

public class UserLocation
{
    public int Id { get; set; }
    public string? Country { get; set; }
    public string? Province { get; set; }
    public string? City { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }

    [JsonIgnore] 
    public User User { get; set; }

}