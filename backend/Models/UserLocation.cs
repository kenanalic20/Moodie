using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace auth.Models
{
    public class UserLocation
    {
        public int Id { get; set; }
        public string? Continent { get; set; }
        public string? State { get; set; }  
        public string? Country { get; set; }
        public string? City { get; set; }
        [ForeignKey("UserInfoId")]
        public int UserInfoId { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}