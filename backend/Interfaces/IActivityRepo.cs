using Moodie.Models;

namespace Moodie.Interfaces;

public interface IActivityRepo
{
   Activity Create(Activity activity);
   List<Activity>GetByUserId(int UserId);
   List<Activity> GetBestMoodActivities(double averageMoodValue, int UserId);
   List<Activity> GetWorstMoodActivities(double averageMoodValue, int UserId);
   
   void Delete(int Id);

}