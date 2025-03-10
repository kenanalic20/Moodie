using Moodie.Models;

namespace Moodie.Interfaces;
public interface IExportDataRepo
{
    ExportData Create(ExportData mood);
    List<ExportData> GetLastSevenDaysByUserId(int userId);
}