using Moodie.Dtos;
using Moodie.Models;

namespace Moodie.Interfaces;

public interface IMoodRepo
{
    Mood Create(Mood mood);
    Mood GetById(int? id);
    double GetAverageMoodValue(int userId);
    List<Mood> GetByUserId(int userId);
    List<MoodExportDto> GetExportByUserId(int userId);
    public void Delete(int id);

    Dictionary<string, double> GetDailyAverageMood(int userId);
}