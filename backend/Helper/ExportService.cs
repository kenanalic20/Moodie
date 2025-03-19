using CsvHelper;
using System.Globalization;
using Moodie.Data;
using System.Text;
using Moodie.Interfaces;
using System.Text.Json;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Moodie.Models;

namespace Moodie.Helper;

public interface IExportService
{
    byte[] ExportCsv(int UserId, string name, string description);
    byte[] ExportJson(int UserId, string name, string description);
    byte[] ExportPdf(int UserId, string name, string description);
}

public class ExportService : IExportService
{
    private readonly IMoodRepo _repositoryMood;
    private readonly IMoodActivityRepo _repositoryMoodActivity;
    private readonly IStatsRepo _repositoryStats;
    private readonly IUserInfoRepo _repositoryUserInfo;
    private readonly IUserLocationRepo _repositoryUserLocation;

    public ExportService(
        IMoodRepo repositoryMood,
        IMoodActivityRepo repositoryMoodActivity,
        IStatsRepo repositoryStats,
        IUserInfoRepo repositoryUserInfo,
        IUserLocationRepo repositoryUserLocation
    )
    {
        _repositoryMood = repositoryMood;
        _repositoryMoodActivity = repositoryMoodActivity;
        _repositoryStats = repositoryStats;
        _repositoryUserInfo = repositoryUserInfo;
        _repositoryUserLocation = repositoryUserLocation;
    }

    public byte[] ExportCsv(int UserId, string name, string description)
    {
        var mood = _repositoryMood.GetExportByUserId(UserId);
        var averageMood = Math.Round(_repositoryMood.GetAverageMoodValue(UserId), 2);
        var bestMoodActivities = string.Join(", ", _repositoryMoodActivity.GetBestMoodActivities(UserId, averageMood).Select(bm => bm.Name));
        var worstMoodActivities = string.Join(", ", _repositoryMoodActivity.GetWorstMoodActivities(UserId, averageMood).Select(wm => wm.Name));
        var userInfo = _repositoryUserInfo.GetByUserId(UserId);
        var userLocation = _repositoryUserLocation.GetByUserId(UserId);
        var stats = _repositoryStats.GetByUserID(UserId);

        using (var memoryStream = new MemoryStream())
        using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            writer.WriteLine($"Name: {name}");
            writer.WriteLine($"Description: {description}");
            writer.WriteLine($"Average mood: {averageMood}");
            if (!string.IsNullOrEmpty(bestMoodActivities))
                writer.WriteLine($"Best mood activities: {bestMoodActivities}");
            if (!string.IsNullOrEmpty(worstMoodActivities))
                writer.WriteLine($"Worst mood activities: {worstMoodActivities}");

            if (userInfo != null)
            {
                writer.WriteLine();
                writer.WriteLine("User Information:");
                writer.WriteLine($"First Name: {userInfo.FirstName}");
                writer.WriteLine($"Last Name: {userInfo.LastName}");
                writer.WriteLine($"Gender: {userInfo.Gender}");
                writer.WriteLine($"Birthday: {userInfo.Birthday?.ToShortDateString()}");
            }

            if (userLocation != null)
            {
                writer.WriteLine();
                writer.WriteLine("User Location:");
                writer.WriteLine($"Country: {userLocation.Country}");
                writer.WriteLine($"Province: {userLocation.Province}");
                writer.WriteLine($"City: {userLocation.City}");
            }

            if (stats != null && stats.Any())
            {
                writer.WriteLine();
                writer.WriteLine("Statistics:");
                writer.WriteLine("Day of Week,Average Mood,Mood Count,Calculation Date");
                foreach (var stat in stats)
                {
                    writer.WriteLine($"{stat.DayOfWeek},{stat.AverageMood:F2},{stat.MoodCount},{stat.CalculationDate.ToShortDateString()}");
                }
            }

            writer.WriteLine();
            writer.WriteLine("Moods:");
            writer.WriteLine("ID,Mood,Date");
            foreach (var m in mood)
            {
                writer.WriteLine($"{m.Id},{m.Mood},{m.Date.ToShortDateString()}");
            }

            writer.Flush();
            return memoryStream.ToArray();
        }
    }

    public byte[] ExportJson(int UserId, string name, string description)
    {
        var moods = _repositoryMood.GetExportByUserId(UserId);
        var averageMood = Math.Round(_repositoryMood.GetAverageMoodValue(UserId), 2);
        var bestMoodActivities = _repositoryMoodActivity.GetBestMoodActivities(UserId, averageMood).Select(bm => bm.Name).ToList();
        var worstMoodActivities = _repositoryMoodActivity.GetWorstMoodActivities(UserId, averageMood).Select(wm => wm.Name).ToList();
        var userInfo = _repositoryUserInfo.GetByUserId(UserId);
        var userLocation = _repositoryUserLocation.GetByUserId(UserId);
        var stats = _repositoryStats.GetByUserID(UserId);

        var exportData = new
        {
            Name = name,
            Description = description,
            AverageMood = averageMood,
            BestMoodActivities = bestMoodActivities,
            WorstMoodActivities = worstMoodActivities,
            UserInfo = userInfo != null ? new
            {
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Gender = userInfo.Gender,
                Birthday = userInfo.Birthday?.ToShortDateString()
            } : null,
            UserLocation = userLocation != null ? new
            {
                Country = userLocation.Country,
                Province = userLocation.Province,
                City = userLocation.City
            } : null,
            Statistics = stats?.Select(stat => new
            {
                DayOfWeek = stat.DayOfWeek,
                AverageMood = stat.AverageMood,
                MoodCount = stat.MoodCount,
                CalculationDate = stat.CalculationDate.ToShortDateString()
            }).ToList(),
            Moods = moods.Select(m => new
            {
                Id = m.Id,
                Mood = m.Mood,
                Date = m.Date
            }).ToList()
        };

        var jsonString = JsonSerializer.Serialize(exportData, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        return Encoding.UTF8.GetBytes(jsonString);
    }

    public byte[] ExportPdf(int UserId, string name, string description)
    {
        var moods = _repositoryMood.GetExportByUserId(UserId);
        var averageMood = Math.Round(_repositoryMood.GetAverageMoodValue(UserId), 2);
        var bestMoodActivities = string.Join(", ", _repositoryMoodActivity.GetBestMoodActivities(UserId, averageMood).Select(bm => bm.Name));
        var worstMoodActivities = string.Join(", ", _repositoryMoodActivity.GetWorstMoodActivities(UserId, averageMood).Select(wm => wm.Name));
        var userInfo = _repositoryUserInfo.GetByUserId(UserId);
        var userLocation = _repositoryUserLocation.GetByUserId(UserId);
        var stats = _repositoryStats.GetByUserID(UserId);

        var pdfBytes = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text(name)
                    .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text(description);

                        column.Item().Text($"Average Mood: {averageMood}");

                        if (!string.IsNullOrEmpty(bestMoodActivities))
                            column.Item().Text($"Best Mood Activities: {bestMoodActivities}");

                        if (!string.IsNullOrEmpty(worstMoodActivities))
                            column.Item().Text($"Worst Mood Activities: {worstMoodActivities}");

                        if (userInfo != null)
                        {
                            column.Item().Text("User Information:").Bold();
                            column.Item().Text($"First Name: {userInfo.FirstName}");
                            column.Item().Text($"Last Name: {userInfo.LastName}");
                            column.Item().Text($"Gender: {userInfo.Gender}");
                            column.Item().Text($"Birthday: {userInfo.Birthday?.ToShortDateString()}");
                        }

                        if (userLocation != null)
                        {
                            column.Item().Text("User Location:").Bold();
                            column.Item().Text($"Country: {userLocation.Country}");
                            column.Item().Text($"Province: {userLocation.Province}");
                            column.Item().Text($"City: {userLocation.City}");
                        }

                        if (stats != null && stats.Any())
                        {
                            column.Item().Text("Statistics:").Bold();
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(); 
                                    columns.RelativeColumn(); 
                                    columns.RelativeColumn(); 
                                    columns.RelativeColumn(); 
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("Day of Week").SemiBold();
                                    header.Cell().Text("Average Mood").SemiBold();
                                    header.Cell().Text("Mood Count").SemiBold();
                                    header.Cell().Text("Calculation Date").SemiBold();
                                });

                                foreach (var stat in stats)
                                {
                                    table.Cell().Text(stat.DayOfWeek);
                                    table.Cell().Text(stat.AverageMood.ToString("F2"));
                                    table.Cell().Text(stat.MoodCount.ToString());
                                    table.Cell().Text(stat.CalculationDate.ToShortDateString());
                                }
                            });
                        }

                        column.Item().Text("Moods:").Bold();
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); 
                                columns.RelativeColumn(); 
                                columns.RelativeColumn(); 
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("ID").SemiBold();
                                header.Cell().Text("Mood").SemiBold();
                                header.Cell().Text("Date").SemiBold();
                            });

                            foreach (var mood in moods)
                            {
                                table.Cell().Text(mood.Id.ToString());
                                table.Cell().Text(mood.Mood);
                                table.Cell().Text(mood.Date.ToShortDateString());
                            }
                        });
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        })
        .GeneratePdf();

        return pdfBytes;
    }
}