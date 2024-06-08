using Moq;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Services;
using SchoolSchedule.Core.Entities;
using SchoolSchedule.Core.Interfaces;

namespace SchoolSchedule.Tests.ServiceTests
{
    public class GradeCategoryServiceTests
    {
        private readonly Mock<IGenericRepository<GradeCategory>> _mockGradeCategoryRepo;
        private readonly GradeCategoryService _service;

        public GradeCategoryServiceTests()
        {
            _mockGradeCategoryRepo = new Mock<IGenericRepository<GradeCategory>>();
            _service = new GradeCategoryService(_mockGradeCategoryRepo.Object);
        }

        [Fact]
        public async Task GetGradeCategoryByIdAsync_ReturnsGradeCategoryDto_WhenGradeCategoryExists()
        {
            // Arrange
            var gradeCategory = new GradeCategory { Id = 1, Name = "Homework" };
            _mockGradeCategoryRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(gradeCategory);

            // Act
            var result = await _service.GetGradeCategoryByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Homework", result.Name);
        }

        [Fact]
        public async Task GetGradeCategoryByIdAsync_ReturnsNull_WhenGradeCategoryDoesNotExist()
        {
            // Arrange
            _mockGradeCategoryRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((GradeCategory)null);

            // Act
            var result = await _service.GetGradeCategoryByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllGradeCategoriesAsync_ReturnsAllGradeCategoryDtos()
        {
            // Arrange
            var gradeCategories = new List<GradeCategory>
            {
                new GradeCategory { Id = 1, Name = "Homework" },
                new GradeCategory { Id = 2, Name = "Exam" }
            };
            _mockGradeCategoryRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(gradeCategories);

            // Act
            var result = await _service.GetAllGradeCategoriesAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddGradeCategoryAsync_AddsGradeCategory()
        {
            // Arrange
            var gradeCategoryDto = new GradeCategoryDto { Id = 1, Name = "Homework" };

            // Act
            await _service.AddGradeCategoryAsync(gradeCategoryDto);

            // Assert
            _mockGradeCategoryRepo.Verify(repo => repo.AddAsync(It.IsAny<GradeCategory>()), Times.Once);
        }

        [Fact]
        public async Task UpdateGradeCategoryAsync_ThrowsException_WhenGradeCategoryDoesNotExist()
        {
            // Arrange
            var gradeCategoryDto = new GradeCategoryDto { Id = 1, Name = "Homework" };
            _mockGradeCategoryRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((GradeCategory)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.UpdateGradeCategoryAsync(gradeCategoryDto));
        }

        [Fact]
        public async Task UpdateGradeCategoryAsync_UpdatesGradeCategory_WhenItExists()
        {
            // Arrange
            var gradeCategory = new GradeCategory { Id = 1, Name = "Homework" };
            var gradeCategoryDto = new GradeCategoryDto { Id = 1, Name = "Updated Homework" };
            _mockGradeCategoryRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(gradeCategory);

            // Act
            await _service.UpdateGradeCategoryAsync(gradeCategoryDto);

            // Assert
            _mockGradeCategoryRepo.Verify(repo => repo.UpdateAsync(It.IsAny<GradeCategory>()), Times.Once);
        }

        [Fact]
        public async Task DeleteGradeCategoryAsync_DeletesGradeCategory_WhenItExists()
        {
            // Act
            await _service.DeleteGradeCategoryAsync(1);

            // Assert
            _mockGradeCategoryRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
