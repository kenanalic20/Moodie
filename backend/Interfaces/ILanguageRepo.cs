using Moodie.Models;

namespace Moodie.Interfaces;

public interface ILanguageRepo
{
    List<Language> GetLanguages();
    Language GetLanguageById(int id);
}

