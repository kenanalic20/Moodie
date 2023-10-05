using auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Moodie.Data;

public interface IUserInfoRepo
{
    UserInfo Create(UserInfo userInfo);
    List<UserInfo> GetByUserId(int userId);
    UserInfo Update(UserInfo userInfo);
    void Delete(int userInfoId);
}