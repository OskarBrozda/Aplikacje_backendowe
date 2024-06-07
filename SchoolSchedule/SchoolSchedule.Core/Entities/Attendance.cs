using CleanArchitectureSolution.Entities;

namespace SchoolSchedule.Core.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public int StudentId { get; set; } 
        public Student Student { get; set; } 
        public int LessonId { get; set; } 
        public Lesson Lesson { get; set; } 
    }
}