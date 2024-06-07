namespace SchoolSchedule.Core.Entities
{
    public class GradeCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}