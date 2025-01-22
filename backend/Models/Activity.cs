using System.ComponentModel.DataAnnotations.Schema;

namespace Moodie.Models;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    
  [ForeignKey("UserId")] public int? UserId { get; set; }
    public User User{get;set;}
}