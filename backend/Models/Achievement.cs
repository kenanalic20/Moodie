namespace Moodie.Models;

public class Achievement
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string BadgeImage { get; set; } // path or byte array
    public int PointValue { get; set; }
    public string Criteria { get; set; } // e.g., "Log mood for 7 consecutive days"
}
