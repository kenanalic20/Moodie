using Moodie.Models;

namespace Moodie.Interfaces;

public interface IMoodRepo
{
    Mood Create(Mood mood);
    Mood GetById(int id);
    double GetAverageMoodValue(int userId);
    List<Mood> GetByUserId(int userId);
    List<Activity> GetAllActivities();
}