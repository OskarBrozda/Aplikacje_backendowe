using SchoolSchedule.Application.DTOs;

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
