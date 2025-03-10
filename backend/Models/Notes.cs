﻿using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Moodie.Models;

public class Notes
{
    public int Id { get; set; }
    public string Title { get; set; }
    public byte[]? Image { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    [ForeignKey("UserId")] public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
    
    // Add relationship to Mood
    public int? MoodId { get; set; }
    [JsonIgnore] public Mood? Mood { get; set; }
}
