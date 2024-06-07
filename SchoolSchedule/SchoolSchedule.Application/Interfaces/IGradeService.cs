using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Interfaces
{
    public interface IGradeService
    {
        Task<GradeDto> GetGradeByIdAsync(int id);
        Task<IEnumerable<GradeDto>> GetAllGradesAsync();
        Task AddGradeAsync(GradeDto gradeDto);
        Task UpdateGradeAsync(GradeDto gradeDto);
        Task DeleteGradeAsync(int id);
    }
}
