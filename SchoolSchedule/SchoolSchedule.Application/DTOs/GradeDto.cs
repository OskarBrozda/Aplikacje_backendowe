namespace SchoolSchedule.Application.DTOs
{
    public class GradeDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int LessonId { get; set; }
        public int GradeCategoryId { get; set; }
    }
}