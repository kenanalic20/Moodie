
using auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Moodie.Data;

public class HabitRepo : IHabitRepo
{
    private readonly ApplicationDbContext _context;

    public HabitRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public Habit Create(Habit habit)
    {
        _context.Habits.Add(habit);
        habit.Id = _context.SaveChanges();
        return habit;
    }

    public Habit GetById(int id)
    {
        return _context.Habits.FirstOrDefault(h => h.Id == id);
    }

    public IEnumerable<Habit> GetByUserId(int userId)
    {
        return _context.Habits.Where(h => h.UserId == userId).ToList();
    }

    public void Update(Habit habit)
    {
        _context.Habits.Update(habit);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var habit = GetById(id);
        if (habit != null)
        {
            _context.Habits.Remove(habit);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Habit> GetAllActive()
    {
        return _context.Habits
            .Include(h => h.User)
            .Where(h => h.IsActive)
            .ToList();
    }
}
