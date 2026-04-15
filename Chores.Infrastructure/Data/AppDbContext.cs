using HousemateChoreReminderAPI.Chores.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace HousemateChoreReminderAPI.Chores.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

      public DbSet<Housemate> Housemates { get; set; }
      public DbSet<Chore> Chores { get; set; }
      public DbSet<Assignment> Assignments { get; set; }
      public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Housemate>()
                .HasIndex(h => h.Username)
                .IsUnique();
        }
    }
}
