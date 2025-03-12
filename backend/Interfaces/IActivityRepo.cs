using Moodie.Models;

namespace Moodie.Interfaces;

public interface IActivityRepo
{
   Activity Create(Activity activity);
   Activity Update(Activity activity);
   List<Activity>GetByUserId(int UserId);
   Activity GetById(int? Id);  
   
   void Delete(int Id);

}