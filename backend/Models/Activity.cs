using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace auth.Models;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    
  [ForeignKey("UserId")] public int? UserId { get; set; }
    public User User{get;set;}
}