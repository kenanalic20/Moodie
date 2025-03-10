using System.ComponentModel.DataAnnotations.Schema;

namespace Moodie.Models;
public class ExportData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Format { get; set; }
    public DateTime Date { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
}