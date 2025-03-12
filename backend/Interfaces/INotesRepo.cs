using Moodie.Models;

namespace Moodie.Interfaces;

public interface INotesRepo
{
    Notes Create(Notes notes);
    Notes GetById(int id);
    List<Notes> GetByUserId(int userId);
    void Delete(int Id);
}