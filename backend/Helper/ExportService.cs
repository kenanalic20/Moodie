using CsvHelper;
using System.Globalization;
using Moodie.Data;
using System.Text;
using Moodie.Interfaces;
using System.Text.Json;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
    private readonly IActivityRepo _repositoryActivity;

    public ExportService(IMoodRepo repositoryMood, IActivityRepo repositoryActivity,IExportDataRepo repositoryExportData)
    {
        _repositoryMood = repositoryMood;
        _repositoryActivity = repositoryActivity;
    }

    public byte[] ExportCsv(int UserId, string name, string description)
    {
        var mood = _repositoryMood.GetExportByUserId(UserId);
        var averageMood = Math.Round(_repositoryMood.GetAverageMoodValue(UserId),2);
        var bestMoodActivities = string.Join(", ", _repositoryActivity.GetBestMoodActivities(averageMood, UserId).Select(bm => bm.Name));
        var worstMoodActivities = string.Join(", ", _repositoryActivity.GetWorstMoodActivities(averageMood, UserId).Select(wm => wm.Name));
        
        var exportData = mood.Select(m=>new {
            Id = m.Id,
            Mood = m.Mood,
            Date = m.Date
        }).ToList();

        using (var memoryStream = new MemoryStream())
        using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            writer.WriteLine($"Name: {name}");
            writer.WriteLine($"Description: {description}");
            writer.WriteLine($"Average mood: {averageMood}");
            if(!string.IsNullOrEmpty(bestMoodActivities))
                writer.WriteLine($"Best mood activities: {bestMoodActivities}");
            if(!string.IsNullOrEmpty(worstMoodActivities))
                writer.WriteLine($"Worst mood activities: {worstMoodActivities}");
            writer.WriteLine();

            csv.WriteRecords(exportData);
            writer.Flush();

            return memoryStream.ToArray();
        }
    }

    public byte[] ExportJson(int UserId, string name, string description)
    {
        var moods = _repositoryMood.GetExportByUserId(UserId);
        var averageMood = Math.Round(_repositoryMood.GetAverageMoodValue(UserId), 2);
        var bestMoodActivities = _repositoryActivity.GetBestMoodActivities(averageMood, UserId).Select(bm => bm.Name).ToList();
        var worstMoodActivities = _repositoryActivity.GetWorstMoodActivities(averageMood, UserId).Select(wm => wm.Name).ToList();

        var exportData = new
        {
            Name = name,
            Description = description,
            AverageMood = averageMood,
            BestMoodActivities = bestMoodActivities,
            WorstMoodActivities = worstMoodActivities,
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
        var bestMoodActivities = string.Join(", ", _repositoryActivity.GetBestMoodActivities(averageMood, UserId).Select(bm => bm.Name));
        var worstMoodActivities = string.Join(", ", _repositoryActivity.GetWorstMoodActivities(averageMood, UserId).Select(wm => wm.Name));

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