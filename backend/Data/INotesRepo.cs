using auth.Models;

namespace Moodie.Data;

public interface INotesRepo
{
    Notes Create(Notes notes);
    Notes GetById(int id);
    List<Notes> GetByUserId(int userId);
    Notes Delete(int userId);
}