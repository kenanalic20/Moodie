using System.ComponentModel.DataAnnotations.Schema;

namespace Moodie.Models;

public class GoalType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [ForeignKey("GoalId")] public int GoalID { get; set; }

    public Goal Goal { get; set; }
}