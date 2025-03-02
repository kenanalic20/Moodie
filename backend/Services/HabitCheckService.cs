
using Moodie.Interfaces;
using Moodie.Helper;

namespace Moodie.Services;

public class HabitCheckService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<HabitCheckService> _logger;

    public HabitCheckService(IServiceProvider services, ILogger<HabitCheckService> logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _services.CreateScope())
            {
                var habitRepo = scope.ServiceProvider.GetRequiredService<IHabitRepo>();
                var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

                var habits = habitRepo.GetAllActive();

                foreach (var habit in habits)
                {
                    if (DateTime.UtcNow - habit.LastCheckIn > TimeSpan.FromHours(24))
                    {
                        habit.CurrentStreak = 0;
                        habit.LastCheckIn = DateTime.UtcNow;
                        habitRepo.Update(habit);

                        emailService.SendHabitMissedEmail(habit.User.Email, habit.Name);
                    }
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
        }
    }
}
