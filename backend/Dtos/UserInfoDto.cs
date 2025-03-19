using Moodie.Models;

namespace Moodie.Dtos;

public class UserInfoDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public DateTime? Birthday { get; set; }
}
