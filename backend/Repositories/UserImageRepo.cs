using Moodie.Models;
using Moodie.Interfaces;
using Moodie.Data;



namespace Moodie.Repositories;

public class UserImageRepo : IUserImageRepo
{
    private readonly ApplicationDbContext _context;

    public UserImageRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserImage Create(UserImage userImage)
    {
        _context.UserImages.Add(userImage);
        _context.SaveChanges();
        return userImage;
    }

    public UserImage Update(UserImage userImage)
    {
        var existingUserImage = _context.UserImages.Find(userImage.Id);
        if (existingUserImage != null)
        {
            existingUserImage.ImagePath = userImage.ImagePath;
            existingUserImage.Status = userImage.Status;
            existingUserImage.Date = userImage.Date;

            _context.SaveChanges();
        }
        return existingUserImage;
    }


    public void Delete(int userId)
    {
        var existingUserImage = _context.UserImages.FirstOrDefault(userImage => userImage.UserId == userId);
       if(existingUserImage!=null) {
            _context.UserImages.Remove(existingUserImage);
            _context.SaveChanges();
       }

    }

    public UserImage GetByUserId(int userId)
    {
        return _context.UserImages
        .FirstOrDefault(ui => ui.UserId == userId);;
    }
}