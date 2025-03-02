using Moodie.Models;

namespace Moodie.Interfaces;

public interface IGoalRepo
{
    Goal Create(Goal goal);
    Goal GetById(int id);
    List<Goal> GetByUserId(int userId);
    Goal Update(Goal goal);
    void Delete(int id);
}
