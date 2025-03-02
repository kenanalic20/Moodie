using Moodie.Models;
using Moodie.Interfaces;
using Moodie.Data;


namespace Moodie.Repositories;

public class SettingsRepo : ISettingsRepo
{
    private readonly ApplicationDbContext _context;

    public SettingsRepo(ApplicationDbContext context)
    {
        _context = context;
    }

   public Settings Create(Settings settings, Settings existingSettings)
    {
        if (existingSettings != null)
        {
            _context.Settings.Remove(existingSettings);
        }
        _context.Settings.Add(settings);
        _context.SaveChanges();
        return settings;
    }
    public Settings Update(Settings settings, int userId)
    {
        var settingsToUpdate = _context.Settings.FirstOrDefault(u => u.UserId == userId);
        if (settingsToUpdate == null) return null;
        settingsToUpdate.DarkMode = settings.DarkMode;
        settingsToUpdate.LanguageId = settings.LanguageId;
        settingsToUpdate.ReducedMotion = settings.ReducedMotion;
        _context.SaveChanges();
        return settingsToUpdate;
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