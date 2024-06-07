using SchoolSchedule.Core.Entities;
using CleanArchitectureSolution.Entities;

namespace SchoolSchedule.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolScheduleContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return; 
            }

            var students = new Student[]
            {
                new Student { Name = "John Doe" },
                new Student { Name = "Jane Smith" },
                new Student { Name = "Samuel Jackson" }
            };

            foreach (var student in students)
            {
                context.Students.Add(student);
            }
            context.SaveChanges();

            var gradeCategories = new GradeCategory[]
            {
                new GradeCategory { Name = "Homework" },
                new GradeCategory { Name = "Test" },
                new GradeCategory { Name = "Exam" }
            };

            foreach (var gradeCategory in gradeCategories)
            {
                context.GradeCategories.Add(gradeCategory);
            }
            context.SaveChanges();

            var lessons = new Lesson[]
            {
                new Lesson { Title = "Math", Description = "Basic Math" },
                new Lesson { Title = "Science", Description = "Physics and Chemistry" },
                new Lesson { Title = "History", Description = "World History" }
            };

            foreach (var lesson in lessons)
            {
                context.Lessons.Add(lesson);
            }
            context.SaveChanges();

            var grades = new Grade[]
            {
                new Grade { Value = 90, LessonId = lessons[0].Id, GradeCategoryId = gradeCategories[1].Id, StudentId = students[0].Id },
                new Grade { Value = 85, LessonId = lessons[1].Id, GradeCategoryId = gradeCategories[2].Id, StudentId = students[1].Id },
                new Grade { Value = 88, LessonId = lessons[2].Id, GradeCategoryId = gradeCategories[0].Id, StudentId = students[2].Id }
            };

            foreach (var grade in grades)
            {
                context.Grades.Add(grade);
            }
            context.SaveChanges();

            var attendances = new Attendance[]
            {
                new Attendance { Type = "Present", Date = DateTime.Now, StudentId = students[0].Id, LessonId = lessons[0].Id },
                new Attendance { Type = "Absent", Date = DateTime.Now, StudentId = students[1].Id, LessonId = lessons[1].Id },
                new Attendance { Type = "Present", Date = DateTime.Now, StudentId = students[2].Id, LessonId = lessons[2].Id }
            };

            foreach (var attendance in attendances)
            {
                context.Attendances.Add(attendance);
            }
            context.SaveChanges();
        }
    }
}
