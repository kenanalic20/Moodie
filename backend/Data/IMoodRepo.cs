using auth.Models;

namespace Moodie.Data;

public interface IMoodRepo
{
    Mood Create(Mood mood);
    Mood GetById(int id);
    List<Mood> GetByUserId(int userId);
    List<Activity> GetAllActivities();
}