using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Interfaces
{
    public interface IGradeCategoryService
    {
        Task<GradeCategoryDto> GetGradeCategoryByIdAsync(int id);
        Task<IEnumerable<GradeCategoryDto>> GetAllGradeCategoriesAsync();
        Task AddGradeCategoryAsync(GradeCategoryDto gradeCategoryDto);
        Task UpdateGradeCategoryAsync(GradeCategoryDto gradeCategoryDto);
        Task DeleteGradeCategoryAsync(int id);
    }
}
