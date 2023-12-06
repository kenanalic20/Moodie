using auth.Models;

namespace Moodie.Data;

public interface IGoalRepo
{
    Goal Create(Goal goal);
    Goal GetById(int id);
    List<Goal> GetByUserId(int userId);
    Goal Update(Goal goal);
}