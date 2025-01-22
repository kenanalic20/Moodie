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
    }
}
