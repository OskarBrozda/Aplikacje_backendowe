namespace CleanArchitectureSolution.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public int GradeCategoryId { get; set; }
        public GradeCategory GradeCategory { get; set; }
    }
}
