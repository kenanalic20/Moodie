namespace Moodie.Dtos;

public class UserImageDto
{
    public string Status { get; set; }
    public IFormFile? Image { get; set; }
}