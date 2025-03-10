using Moodie.Models;
using Moodie.Interfaces;
using Moodie.Data;



namespace Moodie.Repositories;

public class UserInfoRepo : IUserInfoRepo
{
    private readonly ApplicationDbContext _context;

    public UserInfoRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserInfo Create(UserInfo userInfo, int userId)
    {
        var existingUserInfo = _context.UserInfo.FirstOrDefault(u => u.UserId == userId);
        if (existingUserInfo != null)
        {
            existingUserInfo.FirstName = userInfo.FirstName;
            existingUserInfo.LastName = userInfo.LastName;
            existingUserInfo.Gender = userInfo.Gender;
            existingUserInfo.Birthday = userInfo.Birthday;
            _context.UserInfo.Update(existingUserInfo);
            _context.SaveChanges();
            return existingUserInfo;
        }

        _context.UserInfo.Add(userInfo);
        _context.SaveChanges();
        return userInfo;
    }

    public UserInfo GetByUserId(int userId)
    {
        return _context.UserInfo.Where(u => u.UserId == userId).FirstOrDefault();
    }

    public UserInfo Update(UserInfo userInfo)
    {
        _context.UserInfo.Update(userInfo);
        _context.SaveChanges();
        return userInfo;
    }

    public void Delete(int userId)
    {
        var existingUserInfo = _context.UserInfo.FirstOrDefault(u => u.UserId == userId);

        if (existingUserInfo != null)
        {
            _context.UserInfo.Remove(existingUserInfo);
            _context.SaveChanges();
        }
    }
}
