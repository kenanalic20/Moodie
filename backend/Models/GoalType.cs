using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
namespace auth.Models
{
    public class GoalType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description {get; set;}
    }
}
