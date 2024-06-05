using CleanArchitectureSolution.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}