using auth.Models;

namespace Moodie.Data
{
    public interface IUserRepo
    {
        User Create(User user);
        User GetByEmail(string email);
        User GetById(int id);
    }
}

