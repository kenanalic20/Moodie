
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Models

{
    public class Notes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[]? Image { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}


