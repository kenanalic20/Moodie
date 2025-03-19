using Moodie.Models;

namespace Moodie.Interfaces;

public interface IUserInfoRepo
{
    UserInfo Create(UserInfo userInfo);
    UserInfo GetByUserId(int userId);
    UserInfo Update(UserInfo userInfo);
    void Delete(int userId);
    UserInfo Update(UserInfo userInfo, int userId);
}
