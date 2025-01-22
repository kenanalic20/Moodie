
using Moodie.Models;

namespace Moodie.Data;
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
}
        