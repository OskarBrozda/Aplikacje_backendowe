using SchoolSchedule.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSchedule.Application.Interfaces
{
    public interface ILessonService
    {
        Task<LessonDto> GetLessonByIdAsync(int id);
        Task<IEnumerable<LessonDto>> GetAllLessonsAsync();
        Task AddLessonAsync(LessonDto lessonDto);
        Task UpdateLessonAsync(LessonDto lessonDto);
        Task DeleteLessonAsync(int id);
    }
}
