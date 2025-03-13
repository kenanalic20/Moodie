namespace Moodie.Dtos;

public class SettingsDto
{
    public bool? DarkMode { get; set; }
    public int? LanguageId { get; set; }
    public int? UserId { get; set; }
    public bool? TwoFactorEnabled { get; set; }
}
