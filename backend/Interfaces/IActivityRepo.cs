using Moodie.Models;

namespace Moodie.Interfaces;

public interface IActivityRepo
{
   Activity Create(Activity activity);
   List<Activity>GetByUserId(int UserId);
   
   void Delete(int Id);

}