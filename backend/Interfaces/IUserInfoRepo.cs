using Moodie.Models;

namespace Moodie.Interfaces;

public interface IUserInfoRepo
{
    UserInfo Create(UserInfo userInfo, int userId);
    UserInfo GetByUserId(int userId);

    void Delete(int userId);
}