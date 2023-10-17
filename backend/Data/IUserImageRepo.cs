using auth.Models;

namespace Moodie.Data;

public interface IUserImageRepo
{
    UserImage Create(UserImage userImage);
    
    UserImage GetByUserInfoId(int userInfoId);
}