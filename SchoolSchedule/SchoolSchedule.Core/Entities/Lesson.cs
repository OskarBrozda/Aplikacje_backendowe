
namespace SchoolSchedule.Core.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}