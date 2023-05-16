using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<StudentDb> Students { get; set; }
        public DbSet<ProfessorDb> Professors { get; set; }
        public DbSet<LectureDb> Lectures { get; set; }
        public DbSet<StudentAttendanceDb> StudentAttendances { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DatabaseInitializer seeder = new();
            seeder.Seed(modelBuilder);
        }
    }
}