namespace Moodie.Models;

public class Achievement
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string BadgeImage { get; set; }  
    public int PointValue { get; set; }
    public string Criteria { get; set; } 
    public string Slug { get; set; }
}
