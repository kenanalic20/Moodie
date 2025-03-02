
using Moodie.Models;

namespace Moodie.Interfaces;

public interface IHabitRepo
{
    Habit Create(Habit habit);
    Habit GetById(int id);
    IEnumerable<Habit> GetByUserId(int userId);
    void Update(Habit habit);
    void Delete(int id);
    IEnumerable<Habit> GetAllActive();
}
