using auth.Models;

namespace Moodie.Data;

public interface IActivityRepo
{
   Activity Create(Activity activity);
   List<Activity>GetByUserId(int UserId);
}