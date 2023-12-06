using auth.Models;

namespace Moodie.Data;

public interface IUserInfoRepo
{
    UserInfo Create(UserInfo userInfo, int userId);
    UserInfo GetByUserId(int userId);

    void Delete(int userId);
}