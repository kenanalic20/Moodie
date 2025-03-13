using Moodie.Models;

namespace Moodie.Interfaces;

public interface INotesRepo
{
    Notes Create(Notes notes);
    Notes GetById(int id);
    List<Notes> GetByUserId(int userId);
    public Notes Update(Notes notes);
    void Delete(int Id);
}