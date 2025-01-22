using Moodie.Models;

namespace Moodie.Data;

public class SettingsRepo : ISettingsRepo
{
    private readonly ApplicationDbContext _context;

    public SettingsRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public Settings Create(Settings settings, int userId)
    {
        var existingSettings = _context.Settings.FirstOrDefault(s => s.UserId == userId);
        if (existingSettings != null)
        {
            existingSettings.LanguageId = settings.LanguageId;
            existingSettings.DarkMode = settings.DarkMode;
            existingSettings.ReducedMotion = settings.ReducedMotion;
            _context.Settings.Update(existingSettings);
            _context.SaveChanges();
            return existingSettings;
        }

        _context.Settings.Add(settings);
        _context.SaveChanges();
        return settings;
    }

    public Settings GetByUserId(int userId)
    {
        return _context.Settings.FirstOrDefault(u => u.UserId == userId);
    }

    public Settings Delete(int userId)
    {
        var settings = _context.Settings.FirstOrDefault(u => u.UserId == userId);
        _context.Settings.Remove(settings);
        _context.SaveChanges();
        return settings;
    }
}