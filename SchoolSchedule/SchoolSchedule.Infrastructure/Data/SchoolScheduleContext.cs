using CleanArchitectureSolution.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Core.Entities;

namespace SchoolSchedule.Infrastructure.Data
{
    public class SchoolScheduleContext : DbContext
    {
        public SchoolScheduleContext(DbContextOptions<SchoolScheduleContext> options)
            : base(options)
        {
        }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<GradeCategory> GradeCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("SchoolScheduleDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Attendances)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId);

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.Attendances)
                .WithOne(a => a.Lesson)
                .HasForeignKey(a => a.LessonId);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Grades)
                .WithOne(g => g.Student)
                .HasForeignKey(g => g.StudentId);

            modelBuilder.Entity<GradeCategory>()
                .HasMany(gc => gc.Grades)
                .WithOne(g => g.GradeCategory)
                .HasForeignKey(g => g.GradeCategoryId);
        }
    }
}