using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;
using SchoolSchedule.Core.Entities;
using SchoolSchedule.Core.Interfaces;
namespace SchoolSchedule.Application.Services
{
    public class GradeCategoryService : IGradeCategoryService
    {
        private readonly IGenericRepository<GradeCategory> _gradeCategoryRepository;

        public GradeCategoryService(IGenericRepository<GradeCategory> gradeCategoryRepository)
        {
            _gradeCategoryRepository = gradeCategoryRepository;
        }

        public async Task<GradeCategoryDto> GetGradeCategoryByIdAsync(int id)
        {
            var gradeCategory = await _gradeCategoryRepository.GetByIdAsync(id);
            return gradeCategory != null ? new GradeCategoryDto { Id = gradeCategory.Id, Name = gradeCategory.Name } : null;
        }

        public async Task<IEnumerable<GradeCategoryDto>> GetAllGradeCategoriesAsync()
        {
            var gradeCategories = await _gradeCategoryRepository.GetAllAsync();
            return gradeCategories.Select(gc => new GradeCategoryDto { Id = gc.Id, Name = gc.Name });
        }

        public async Task AddGradeCategoryAsync(GradeCategoryDto gradeCategoryDto)
        {
            var gradeCategory = new GradeCategory { Name = gradeCategoryDto.Name };
            await _gradeCategoryRepository.AddAsync(gradeCategory);
        }

        public async Task UpdateGradeCategoryAsync(GradeCategoryDto gradeCategoryDto)
        {
            var gradeCategory = await _gradeCategoryRepository.GetByIdAsync(gradeCategoryDto.Id);
            if (gradeCategory == null)
            {
                throw new Exception("Grade category not found");
            }

            gradeCategory.Name = gradeCategoryDto.Name;
            await _gradeCategoryRepository.UpdateAsync(gradeCategory);
        }

        public async Task DeleteGradeCategoryAsync(int id)
        {
            await _gradeCategoryRepository.DeleteAsync(id);
        }
    }
}
