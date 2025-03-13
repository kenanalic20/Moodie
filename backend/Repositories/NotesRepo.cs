using Moodie.Models;
using Moodie.Interfaces;
using Moodie.Data;
using Moodie.Helper;



namespace Moodie.Repositories;

public class NotesRepo : INotesRepo
{
    private readonly ApplicationDbContext _context;
    private readonly ImageHelper _imageHelper;
    

    public NotesRepo(ApplicationDbContext context,ImageHelper imageHelper)
    {
        _context = context;
        _imageHelper = imageHelper;
    }

    public Notes Create(Notes notes)
    {
        _context.Notes.Add(notes);
        _context.SaveChanges();
        return notes;
    }

    public Notes GetById(int id)
    {
        return _context.Notes.Find(id);
    }

    public List<Notes> GetByUserId(int userId)
    {
        return _context.Notes.Where(u => u.UserId == userId).ToList();
    }

    public void Delete(int Id)
    {
        var notes = _context.Notes.Find(Id);
        if (notes != null)
        {
           if (!string.IsNullOrEmpty(notes.ImagePath))
            {
                _imageHelper.DeleteImage(notes.ImagePath);
            }
            _context.Notes.Remove(notes);
            _context.SaveChanges();
        }
    }

    public Notes Update(Notes notes) {
        _context.Notes.Update(notes);
        _context.SaveChanges();
        return notes;
    }
}