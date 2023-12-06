using System.Text.Json.Serialization;

namespace auth.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsVerifiedEmail { get; set; }
    public string EmailToken { get; set; }
    public string? EmailTwoStepToken { get; set; }
    [JsonIgnore] public string Password { get; set; }
    [JsonIgnore] public List<Mood> Moods { get; set; }
    [JsonIgnore] public List<Notes> Notes { get; set; }
}