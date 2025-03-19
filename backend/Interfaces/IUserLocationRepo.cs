using Moodie.Models;

namespace Moodie.Interfaces;

public interface IUserLocationRepo
{
    UserLocation Create(UserLocation userInfo);
    UserLocation GetByUserId(int userId);
    UserLocation Update(UserLocation userImage);
    void Delete(int userId);
}
