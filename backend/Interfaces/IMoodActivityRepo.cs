using Moodie.Models;

namespace Moodie.Interfaces;
public interface IMoodActivityRepo
{
    public MoodActivity Create(MoodActivity moodActivity);
    List<MoodActivity> GetAllMoodActivities(int userId);

    MoodActivity? GetMoodActivityByMoodAndActivityId(int moodId, int activityId, int userId);

    public List<Activity> GetActivitiesByMoodId(int moodId, int userId);

    public List<Activity> GetBestMoodActivities(int userId);

    public List<Activity> GetWorstMoodActivities(int userId);

    void Delete(MoodActivity moodActivity);


}