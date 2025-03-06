namespace Moodie.Dtos;

public class NotesDto
{
    public string? Title { get; set; }
    public IFormFile? Image { get; set; }
    public string? Description { get; set; }
}