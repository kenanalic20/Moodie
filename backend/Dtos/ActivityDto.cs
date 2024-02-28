using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace auth.Models;

public class ActivityDto
{
    public string Name { get; set; }
    public string ?Description { get; set; }
    
}