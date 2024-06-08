using Moq;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Services;
using SchoolSchedule.Core.Entities;
using SchoolSchedule.Core.Interfaces;

namespace SchoolSchedule.Tests.ServiceTests
{
    public class LessonServiceTests
    {
        private readonly Mock<IGenericRepository<Lesson>> _mockLessonRepo;
        private readonly LessonService _service;

        public LessonServiceTests()
        {
            _mockLessonRepo = new Mock<IGenericRepository<Lesson>>();
            _service = new LessonService(_mockLessonRepo.Object);
        }

        [Fact]
        public async Task GetLessonByIdAsync_ReturnsLessonDto_WhenLessonExists()
        {
            // Arrange
            var lesson = new Lesson { Id = 1, Title = "Math", Description = "Math lesson" };
            _mockLessonRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(lesson);

            // Act
            var result = await _service.GetLessonByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Math", result.Title);
        }

        [Fact]
        public async Task GetLessonByIdAsync_ReturnsNull_WhenLessonDoesNotExist()
        {
            // Arrange
            _mockLessonRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Lesson)null);

            // Act
            var result = await _service.GetLessonByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllLessonsAsync_ReturnsAllLessonDtos()
        {
            // Arrange
            var lessons = new List<Lesson>
            {
                new Lesson { Id = 1, Title = "Math", Description = "Math lesson" },
                new Lesson { Id = 2, Title = "Science", Description = "Science lesson" }
            };
            _mockLessonRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(lessons);

            // Act
            var result = await _service.GetAllLessonsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddLessonAsync_AddsLesson()
        {
            // Arrange
            var lessonDto = new LessonDto { Id = 1, Title = "Math", Description = "Math lesson" };

            // Act
            await _service.AddLessonAsync(lessonDto);

            // Assert
            _mockLessonRepo.Verify(repo => repo.AddAsync(It.IsAny<Lesson>()), Times.Once);
        }

        [Fact]
        public async Task UpdateLessonAsync_ThrowsException_WhenLessonDoesNotExist()
        {
            // Arrange
            var lessonDto = new LessonDto { Id = 1, Title = "Math", Description = "Math lesson" };
            _mockLessonRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Lesson)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.UpdateLessonAsync(lessonDto));
        }

        [Fact]
        public async Task UpdateLessonAsync_UpdatesLesson_WhenItExists()
        {
            // Arrange
            var lesson = new Lesson { Id = 1, Title = "Math", Description = "Math lesson" };
            var lessonDto = new LessonDto { Id = 1, Title = "Advanced Math", Description = "Advanced math lesson" };
            _mockLessonRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(lesson);

            // Act
            await _service.UpdateLessonAsync(lessonDto);

            // Assert
            _mockLessonRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Lesson>()), Times.Once);
        }

        [Fact]
        public async Task DeleteLessonAsync_DeletesLesson_WhenItExists()
        {
            // Act
            await _service.DeleteLessonAsync(1);

            // Assert
            _mockLessonRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
