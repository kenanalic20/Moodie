using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
namespace auth.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public bool DarkMode { get; set; }
        public string Language { get; set; }
        public bool ReducedMotion { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [JsonIgnore]  public User User { get; set; }


    }
}
