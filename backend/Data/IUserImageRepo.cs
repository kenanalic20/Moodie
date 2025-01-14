using Moodie.Models;

namespace Moodie.Data;

public interface IUserImageRepo
{
    UserImage Create(UserImage userImage, int userInfoId);

    UserImage GetByUserInfoId(int userInfoId);

    UserImage Delete(int userInfoId);
}