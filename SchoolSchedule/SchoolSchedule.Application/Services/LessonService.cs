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
    public class LessonService : ILessonService
    {
        private readonly IGenericRepository<Lesson> _lessonRepository;

        public LessonService(IGenericRepository<Lesson> lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<LessonDto> GetLessonByIdAsync(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            return lesson != null ? new LessonDto { Id = lesson.Id, Title = lesson.Title, Description = lesson.Description } : null;
        }

        public async Task<IEnumerable<LessonDto>> GetAllLessonsAsync()
        {
            var lessons = await _lessonRepository.GetAllAsync();
            return lessons.Select(lesson => new LessonDto { Id = lesson.Id, Title = lesson.Title, Description = lesson.Description });
        }

        public async Task AddLessonAsync(LessonDto lessonDto)
        {
            var lesson = new Lesson { Title = lessonDto.Title, Description = lessonDto.Description };
            await _lessonRepository.AddAsync(lesson);
        }

        public async Task UpdateLessonAsync(LessonDto lessonDto)
        {
            var lesson = new Lesson { Id = lessonDto.Id, Title = lessonDto.Title, Description = lessonDto.Description };
            await _lessonRepository.UpdateAsync(lesson);
        }

        public async Task DeleteLessonAsync(int id)
        {
            await _lessonRepository.DeleteAsync(id);
        }
    }
}
