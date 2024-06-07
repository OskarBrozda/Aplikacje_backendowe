namespace SchoolSchedule.Application.DTOs
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public int StudentId { get; set; } 
        public int LessonId { get; set; }
    }
}