using Moodie.Models;

namespace Moodie.Interfaces;

public interface IUserInfoRepo
{
    UserInfo Create(UserInfo userInfo, int userId);
    UserInfo GetByUserId(int userId);
    UserInfo Update(UserInfo userInfo);
    void Delete(int userId);
}
