using auth.Models;

namespace Moodie.Data;

public class NotesRepo : INotesRepo
{
    private readonly ApplicationDbContext _context;

    public NotesRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public Notes Create(Notes notes)
    {
        _context.Notes.Add(notes);
        notes.Id = _context.SaveChanges();
        return notes;
    }

    public Notes GetById(int id)
    {
        return _context.Notes.FirstOrDefault(u => u.Id == id);
    }

    public List<Notes> GetByUserId(int userId)
    {
        return _context.Notes.Where(u => u.UserId == userId).ToList();
    }

    public Notes Delete(int userId)
    {
        var notes = _context.Notes.FirstOrDefault(u => u.UserId == userId);
        if (notes != null)
        {
            _context.Notes.Remove(notes);
            _context.SaveChanges();
        }

        return notes;
    }
}