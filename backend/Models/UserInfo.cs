using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Moodie.Models;

public class UserInfo
{
    [Key]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public DateTime? Birthday { get; set; }
    
    public string? ProfilePhoto { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
}
