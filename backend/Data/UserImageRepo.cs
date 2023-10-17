using auth.Models;

namespace Moodie.Data;

public class UserImageRepo: IUserImageRepo
{
    private readonly ApplicationDbContext _context;

    public UserImageRepo (ApplicationDbContext context)
    {
        _context = context;
    }

    public UserImage Create(UserImage userImage)
    {
        _context.UserImages.Add(userImage);
        _context.SaveChanges();
        return userImage;
    }
    
    
    
    public UserImage GetByUserInfoId(int userInfoId)
    {
        return _context.UserImages.Where(userImage => userImage.UserInfoId == userInfoId).FirstOrDefault();
    }
}


