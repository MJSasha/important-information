using ImpInfCommon.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImpInfApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<Day> Days { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonsAndTimes> LessonsAndTimes { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasOne(n => n.Day).WithMany(d => d.Notes).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.ChatId).IsUnique();

            modelBuilder.Entity<LessonsAndTimes>().HasAlternateKey(lt => new { lt.LessonId, lt.Time, lt.Type });
        }
    }
}
