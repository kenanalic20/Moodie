using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Moodie.Models;

public class Settings
{
    public int Id { get; set; }
    public bool DarkMode { get; set; }
    [ForeignKey("LanguageId")]
    public int LanguageId { get; set; }
    public bool TwoFactorEnabled { get; set; }

    [ForeignKey("UserId")] public int UserId { get; set; }

    [JsonIgnore] public User User { get; set; }
}
