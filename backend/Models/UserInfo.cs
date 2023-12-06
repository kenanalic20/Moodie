using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace auth.Models;

public class UserInfo
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public DateTime? Birthday { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
}