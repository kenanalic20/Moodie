
using Moodie.Models;
using Moodie.Interfaces;
using Moodie.Data;



namespace Moodie.Repositories;
public class LanguageRepo:ILanguageRepo
{
    private readonly ApplicationDbContext _context;
    public LanguageRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Language> GetLanguages()
    {
        return _context.Languages.ToList();
    }
    public Language GetLanguageById(int id) {
        return _context.Languages.Find(id);
    }
}
        