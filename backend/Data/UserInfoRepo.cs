using auth.Models;

namespace Moodie.Data
{

    public class UserInfoRepo : IUserInfoRepo
    {
        private readonly ApplicationDbContext _context;

        public UserInfoRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserInfo Create(UserInfo userInfo)
        {
            _context.UserInfo.Add(userInfo);
            userInfo.Id = _context.SaveChanges();
            return userInfo;
        }

        public List<UserInfo> GetByUserId(int userId)
        {

            return _context.UserInfo.Where(u => u.UserId == userId).ToList();
            
        }

        public UserInfo Update(UserInfo userInfo)
        {
           //write logic for update
           _context.UserInfo.Update(userInfo);
           userInfo.Id = _context.SaveChanges();
           return userInfo;
        }

        public void Delete(int userInfoId)
        {
            var userInfoToDelete = _context.UserInfo.Find(userInfoId);
    
            if (userInfoToDelete != null)
            {
                _context.UserInfo.Remove(userInfoToDelete);
                _context.SaveChanges();
            }
        }
    }
}



