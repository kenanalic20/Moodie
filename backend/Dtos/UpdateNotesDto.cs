namespace Moodie.Dtos;
public class UpdateNotesDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IFormFile? Image { get; set; }
    public string Description { get; set; }
    public int? MoodId { get; set; }
}
