using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
namespace auth.Models
{
    public class Mood
    {
        public int Id { get; set; }
        public double MoodValue { get; set; }
        
        public DateTime Date { get; set; }

        [ForeignKey("UserId")] 
        public int UserId { get; set; }
        [JsonIgnore]  public User User { get; set; }
        
    }
}
