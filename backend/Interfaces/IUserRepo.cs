using Moodie.Models;

namespace Moodie.Interfaces;

public interface IUserRepo
{
    User Create(User user);
    User GetByEmail(string email);
    User GetById(int id);
    User GetByEmailToken(string token);
    User Update(User user);
    User GetByUsername(string username);
}