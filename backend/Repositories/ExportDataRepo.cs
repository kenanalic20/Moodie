using Moodie.Data;
using Moodie.Interfaces;
using Moodie.Models;

public class ExportDataRepo:IExportDataRepo{
    private readonly ApplicationDbContext _context;

    public ExportDataRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    public ExportData Create(ExportData exportData) {
        _context.Exports.Add(exportData);
        _context.SaveChanges();
        return exportData;
    }
    public List<ExportData> GetLastSevenDaysByUserId(int userId)
    {
        var sevenDaysAgo = DateTime.Now.AddDays(-7);
        return _context.Exports
                       .Where(e => e.UserId == userId && e.Date >= sevenDaysAgo)
                       .ToList();
    }
}