using auth.Models;

namespace Moodie.Data;

public class UserImageRepo: IUserImageRepo
{
    private readonly ApplicationDbContext _context;

    public UserImageRepo (ApplicationDbContext context)
    {
        _context = context;
    }

    public UserImage Create(UserImage userImage,int userInfoId)
    {
        var existingUserImage = _context.UserImages.FirstOrDefault(userImage => userImage.UserInfoId == userInfoId);
        if (existingUserImage != null)
        {
           existingUserImage.Status = userImage.Status;
           existingUserImage.Image = userImage.Image;
           
            _context.UserImages.Update(existingUserImage);
            _context.SaveChanges();
            return existingUserImage;
        }
        else
        {
            _context.UserImages.Add(userImage);
            _context.SaveChanges();
            return userImage;
        }
    }
    
    
    public UserImage Delete(int userInfoId)
    {
        var existingUserImage = _context.UserImages.FirstOrDefault(userImage => userImage.UserInfoId == userInfoId);
        if (existingUserImage != null)
        {
            _context.UserImages.Remove(existingUserImage);
            _context.SaveChanges();
        }
        return existingUserImage;
    }
    
    public UserImage GetByUserInfoId(int userInfoId)
    {
        return _context.UserImages.Where(userImage => userImage.UserInfoId == userInfoId).FirstOrDefault();
    }
}


