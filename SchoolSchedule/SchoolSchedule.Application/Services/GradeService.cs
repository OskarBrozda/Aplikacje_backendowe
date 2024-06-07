using CleanArchitectureSolution.Entities;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;
using SchoolSchedule.Core.Entities;
using SchoolSchedule.Core.Interfaces;

namespace SchoolSchedule.Application.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGenericRepository<Grade> _gradeRepository;
        private readonly IGenericRepository<Student> _studentRepository;

        public GradeService(IGenericRepository<Grade> gradeRepository, IGenericRepository<Student> studentRepository)
        {
            _gradeRepository = gradeRepository;
            _studentRepository = studentRepository;
        }

        public async Task<GradeDto> GetGradeByIdAsync(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);
            return grade != null ? new GradeDto
            {
                Id = grade.Id,
                Value = grade.Value,
                LessonId = grade.LessonId,
                StudentId = grade.StudentId
            } : null;
        }

        public async Task<IEnumerable<GradeDto>> GetAllGradesAsync()
        {
            var grades = await _gradeRepository.GetAllAsync();
            return grades.Select(grade => new GradeDto
            {
                Id = grade.Id,
                Value = grade.Value,
                LessonId = grade.LessonId,
                StudentId = grade.StudentId
            });
        }

        public async Task AddGradeAsync(GradeDto gradeDto)
        {
            var student = await _studentRepository.GetByIdAsync(gradeDto.StudentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }

            var grade = new Grade
            {
                Value = gradeDto.Value,
                LessonId = gradeDto.LessonId,
                StudentId = gradeDto.StudentId
            };

            await _gradeRepository.AddAsync(grade);
        }

        public async Task UpdateGradeAsync(GradeDto gradeDto)
        {
            var grade = await _gradeRepository.GetByIdAsync(gradeDto.Id);
            if (grade == null)
            {
                throw new Exception("Grade not found");
            }

            grade.Value = gradeDto.Value;
            grade.LessonId = gradeDto.LessonId;
            grade.StudentId = gradeDto.StudentId;

            await _gradeRepository.UpdateAsync(grade);
        }

        public async Task DeleteGradeAsync(int id)
        {
            await _gradeRepository.DeleteAsync(id);
        }
    }
}

