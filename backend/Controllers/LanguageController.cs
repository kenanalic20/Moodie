using Microsoft.AspNetCore.Mvc;
using Moodie.Helper;
using Moodie.Interfaces;


namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class LanguageController : Controller
{
    private readonly ILanguageRepo _repositoryLanguage;
    private readonly AuthHelper _authHelper;
    public LanguageController(ILanguageRepo repositoryLanguage, AuthHelper authHelper)
    {
        _repositoryLanguage = repositoryLanguage;
        _authHelper = authHelper;
    }

    [HttpGet("languages")]
    public IActionResult GetLanguages()
    {
        if(!_repositoryLanguage.GetLanguages().Any()) return NotFound("No languages found.");

        var languages = _repositoryLanguage.GetLanguages();

        return Ok(languages);
    }

    [HttpGet("languages/{id}")]
    public IActionResult GetLanguagesById(int id)
    {
        if(!_authHelper.IsUserLoggedIn(Request,out var userId)) return Unauthorized("Invalid or expired token.");
        
        if(_repositoryLanguage.GetLanguageById(id)==null) return NotFound("No languages found.");

        var languages = _repositoryLanguage.GetLanguageById(id);

        return Ok(languages);
    }
    
    
}