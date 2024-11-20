
using auth.Models;

namespace Moodie.Data;

public interface IHabitRepo
{
    Habit Create(Habit habit);
    Habit GetById(int id);
    IEnumerable<Habit> GetByUserId(int userId);
    void Update(Habit habit);
    void Delete(int id);
    IEnumerable<Habit> GetAllActive();
}
