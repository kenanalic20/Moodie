using auth.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Moodie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Mood> Moods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notes> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity=>
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
            modelBuilder.Entity<Notes>(entity=>
            {
                entity.HasOne(n => n.User)
                    .WithMany(u => u.Notes)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
                
        }



    }
}
