using Microsoft.AspNetCore.Mvc;
using Moodie.Backend.Dtos;
using Moodie.Helper;
using Moodie.Interfaces;
using Moodie.Models;

namespace Moodie.Controllers;
[Route("api")]
[ApiController]
public class ExportDataController : ControllerBase
{
    private readonly AuthHelper _authHelper;
    private readonly IExportService _exportService;
    private readonly IExportDataRepo _repositoryExportData;

    public ExportDataController(AuthHelper authHelper, IExportService exportService, IExportDataRepo repositoryExportData) {
        _authHelper = authHelper;
        _exportService = exportService;
        _repositoryExportData = repositoryExportData;
    }

    [HttpPost("export")]
    public IActionResult ExportData(ExportDataDto e)
    {
        if(!_authHelper.IsUserLoggedIn(Request,out var userId)) return Unauthorized("Invalid or expired token.");

        if (string.IsNullOrEmpty(e.Name))
            return BadRequest("You didn't set the name of your export.");

        if (string.IsNullOrEmpty(e.Description))
            return BadRequest("You didn't set the description for your export.");

        if (string.IsNullOrEmpty(e.Format))
            return BadRequest("You didn't set the file format.");

        byte[] fileBytes;
        string contentType;
        string fileDownloadName;

        switch (e.Format.ToLower())
        {
            case "csv":
                fileBytes = _exportService.ExportCsv(userId, e.Name, e.Description);
                contentType = "text/csv";
                fileDownloadName = $"{e.Name}.csv";
                break;

            case "json":
                fileBytes = _exportService.ExportJson(userId, e.Name, e.Description);
                contentType = "application/json";
                fileDownloadName = $"{e.Name}.json";
                break;

            case "pdf":
                fileBytes = _exportService.ExportPdf(userId, e.Name, e.Description);
                contentType = "application/pdf";
                fileDownloadName = $"{e.Name}.pdf";
                break;

            default:
                return BadRequest("Unsupported file format.");
        }

        _repositoryExportData.Create(new ExportData{
            Name = e.Name,
            Description = e.Description,
            Format = e.Format,
            Date = DateTime.Now,
            UserId = userId
        });
        
        return File(fileBytes,contentType,fileDownloadName);
    }

    [HttpGet("export")]
    public IActionResult GetExportData() {
        if(!_authHelper.IsUserLoggedIn(Request,out var userId)) return Unauthorized("Invalid or expired token.");

        var exportData = _repositoryExportData.GetLastSevenDaysByUserId(userId);

        if (exportData.Count == 0)
            return NotFound("Data not found");
        
        return Ok(exportData);
    }
    
}