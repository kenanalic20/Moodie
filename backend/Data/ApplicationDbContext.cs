using Moodie.Models;
using Microsoft.EntityFrameworkCore;


namespace Moodie.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Mood> Moods { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Notes> Notes { get; set; }
    public DbSet<UserInfo> UserInfo { get; set; }
    public DbSet<Goal> Goal { get; set; }
    public DbSet<GoalType> GoalType { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
    public DbSet<UserLocation> UserLocations { get; set; }
    public DbSet<Activity> Activity { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<ExportData> Exports { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<UserAchievement> UserAchievements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Password).IsRequired();
        });

        modelBuilder.Entity<User>()
            .HasOne(u => u.Settings)
            .WithOne(s => s.User)
            .HasForeignKey<Settings>(s => s.UserId);

        modelBuilder.Entity<Mood>()
            .HasOne(m => m.User)
            .WithMany(u => u.Moods)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Notes>(entity =>
        {
            entity.HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Habit>()
            .HasOne(h => h.User)
            .WithMany(u => u.Habits)
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Habit>(entity =>
        {
            entity.ToTable("Habits");
            entity.HasKey(e => e.Id);
            entity.HasOne(h => h.User)
                  .WithMany(u => u.Habits)
                  .HasForeignKey(h => h.UserId);
        });
        modelBuilder.Entity<Language>().HasData(
            new Language
            {
                Id = 1,
                Name = "English",
                Code = "en",
                Region = "US"
            },
            new Language
            {
                Id = 2,
                Name = "Bosnian",
                Code = "bs",
                Region = "BA"
            }
        );

        modelBuilder.Entity<UserAchievement>()
            .HasOne(ua => ua.Achievement)
            .WithMany()
            .HasForeignKey(ua => ua.AchievementId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserAchievement>()
            .HasOne(ua => ua.User)
            .WithMany()
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Restrict);
            
        modelBuilder.Entity<Achievement>().HasData(
            new Achievement
            {
                Id = 1,
                Name = "First Mood",
                Description = "You've logged your first mood!",
                BadgeImage = "/images/badges/first-mood.png",
                PointValue = 10,
                Criteria = "Log first mood",
                Slug = "1_mood"
            },
            new Achievement
            {
                Id = 2,
                Name = "Mood Tracker",
                Description = "You've logged 10 moods!",
                BadgeImage = "/images/badges/mood-tracker.png",
                PointValue = 25,
                Criteria = "Log 10 moods",
                Slug = "10_mood"
            },
            new Achievement
            {
                Id = 3,
                Name = "Mood Master",
                Description = "You've logged 50 moods!",
                BadgeImage = "/images/badges/mood-master.png",
                PointValue = 100,
                Criteria = "Log 50 moods",
                Slug = "50_mood"
            },
            new Achievement
            {
                Id = 4,
                Name = "Multilingual",
                Description = "You've switched the app language!",
                BadgeImage = "/images/badges/multilingual.png",
                PointValue = 15,
                Criteria = "Change app language",
                Slug = "switched_language"
            },
            new Achievement
            {
                Id = 5,
                Name = "Habit Former",
                Description = "You've added your first habit to track!",
                BadgeImage = "/images/badges/habit-former.png",
                PointValue = 20,
                Criteria = "Create a habit",
                Slug = "added_habit"
            },
            new Achievement
            {
                Id = 6,
                Name = "Activity Tracker",
                Description = "You've logged your first activity!",
                BadgeImage = "/images/badges/activity-tracker.png",
                PointValue = 15,
                Criteria = "Add an activity",
                Slug = "added_activity"
            },
            new Achievement
            {
                Id = 7,
                Name = "Note Taker",
                Description = "You've added your first note!",
                BadgeImage = "/images/badges/note-taker.png",
                PointValue = 15,
                Criteria = "Add a note",
                Slug = "added_note"
            }
        );
    }
}
