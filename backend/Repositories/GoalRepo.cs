using Moodie.Models;
using Moodie.Interfaces;
using Moodie.Data;


namespace Moodie.Repositories;

public class GoalRepo : IGoalRepo
{
    private readonly ApplicationDbContext _context;

    public GoalRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public Goal Create(Goal Goal)
    {
        _context.Goal.Add(Goal);
        Goal.Id = _context.SaveChanges();
        return Goal;
    }

    public Goal GetById(int id)
    {
        return _context.Goal.FirstOrDefault(u => u.Id == id);
    }

    public List<Goal> GetByUserId(int userId)
    {
        return _context.Goal.Where(u => u.UserId == userId).ToList();
    }

    public Goal Update(Goal Goal)
    {
        _context.Goal.Update(Goal);
        _context.SaveChanges();
        return Goal;
    }

    public void Delete(int id)
    {
        var goal = _context.Goal.Find(id);
        if (goal != null)
        {
            _context.Goal.Remove(goal);
            _context.SaveChanges();
        }
    }
}
