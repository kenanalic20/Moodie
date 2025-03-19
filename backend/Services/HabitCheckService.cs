using Moodie.Interfaces;
using Moodie.Helper;

namespace Moodie.Services;

public class HabitCheckService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<HabitCheckService> _logger;
    private readonly TimeZoneInfo _bosnianTimeZone;

    public HabitCheckService(IServiceProvider services, ILogger<HabitCheckService> logger)
    {
        _services = services;
        _logger = logger;
        _bosnianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("HabitCheckService started at: {time}", DateTimeOffset.Now);
        
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("HabitCheckService processing cycle started at: {time}", DateTimeOffset.Now);
                
                try
                {
                    using (var scope = _services.CreateScope())
                    {
                        var habitRepo = scope.ServiceProvider.GetRequiredService<IHabitRepo>();
                        var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

                        var habits = habitRepo.GetAllActive();
                        _logger.LogInformation("Processing {count} active habits", habits.Count());

                        var bosnianNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _bosnianTimeZone);
                        _logger.LogInformation("Current Bosnian time: {time}", bosnianNow);

                        int resetCount = 0;
                        foreach (var habit in habits)
                        {
                            if ((bosnianNow - habit.LastCheckIn).TotalHours > 24)
                            {
                                _logger.LogInformation("Resetting streak for habit: {habitName} (ID: {habitId})", habit.Name, habit.Id);
                                habit.CurrentStreak = 0;
                                habit.LastCheckIn = bosnianNow;
                                habitRepo.Update(habit);

                                _logger.LogInformation("Sending missed habit email to: {email}", habit.User.Email);
                                emailService.SendHabitMissedEmail(habit.User.Email, habit.Name);
                                resetCount++;
                            }
                        }
                        _logger.LogInformation("Reset streaks for {count} habits", resetCount);
                    }

                    _logger.LogInformation("HabitCheckService sleeping for 30 minutes until: {time}", DateTimeOffset.Now.AddMinutes(30));
                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while processing habits");
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Fatal error in HabitCheckService. Service stopping.");
            throw;
        }
        finally
        {
            _logger.LogInformation("HabitCheckService stopping at: {time}", DateTimeOffset.Now);
        }
    }
}
