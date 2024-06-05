using CleanArchitectureSolution.Entities;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;
using SchoolSchedule.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSchedule.Application.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGenericRepository<Grade> _gradeRepository;

        public GradeService(IGenericRepository<Grade> gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<GradeDto> GetGradeByIdAsync(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);
            return grade != null ? new GradeDto { Id = grade.Id, Value = grade.Value, LessonId = grade.LessonId, GradeCategoryId = grade.GradeCategoryId } : null;
        }

        public async Task<IEnumerable<GradeDto>> GetAllGradesAsync()
        {
            var grades = await _gradeRepository.GetAllAsync();
            return grades.Select(grade => new GradeDto { Id = grade.Id, Value = grade.Value, LessonId = grade.LessonId, GradeCategoryId = grade.GradeCategoryId });
        }

        public async Task AddGradeAsync(GradeDto gradeDto)
        {
            var grade = new Grade { Value = gradeDto.Value, LessonId = gradeDto.LessonId, GradeCategoryId = gradeDto.GradeCategoryId };
            await _gradeRepository.AddAsync(grade);
        }

        public async Task UpdateGradeAsync(GradeDto gradeDto)
        {
            var grade = new Grade { Id = gradeDto.Id, Value = gradeDto.Value, LessonId = gradeDto.LessonId, GradeCategoryId = gradeDto.GradeCategoryId };
            await _gradeRepository.UpdateAsync(grade);
        }

        public async Task DeleteGradeAsync(int id)
        {
            await _gradeRepository.DeleteAsync(id);
        }
    }
}
