using Moodie.Models;

namespace Moodie.Interfaces;

public interface IUserImageRepo
{
    UserImage Create(UserImage userImage);
    UserImage Update(UserImage userImage);
    UserImage GetByUserId(int userId);
    void Delete(int userInfoId);
}