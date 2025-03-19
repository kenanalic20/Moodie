using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Moodie.Models;

public class UserImage
{
    public int Id { get; set; }
    public string? ImagePath { get; set; }
    public string? Status { get; set; }
    public DateTime Date { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }

    [JsonIgnore] 
    public User User { get; set; }

}