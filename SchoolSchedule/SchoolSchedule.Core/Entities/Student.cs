namespace CleanArchitectureSolution.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
