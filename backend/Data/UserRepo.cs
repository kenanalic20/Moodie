using auth.Models;

namespace Moodie.Data
{
    public class UserRepo:IUserRepo
    {
        private readonly  ApplicationDbContext _context;
        public UserRepo( ApplicationDbContext context)
        {
            _context = context;
        }
        public User Create(User user)
        {
            
            _context.Users.Add(user);
           user.Id=_context.SaveChanges();
            return user;
        }
        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}

