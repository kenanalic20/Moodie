using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;


namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class LanguageController : Controller
{
    private readonly ILanguageRepo _repositoryLanguage;
    public LanguageController(ILanguageRepo repositoryLanguage)
    {
        _repositoryLanguage = repositoryLanguage;
    }

    [HttpGet("languages")]
    public IActionResult Getlanguages()
    {
        if(!_repositoryLanguage.GetLanguages().Any()) return NotFound("No languages found.");

        var languages = _repositoryLanguage.GetLanguages();

        return Ok(languages);
    }
    
    
}