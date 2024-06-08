using Moq;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Services;
using SchoolSchedule.Core.Entities;
using SchoolSchedule.Core.Interfaces;
using CleanArchitectureSolution.Entities;

namespace SchoolSchedule.Tests.ServiceTests
{
    public class GradeServiceTests
    {
        private readonly Mock<IGenericRepository<Grade>> _mockGradeRepo;
        private readonly Mock<IGenericRepository<Student>> _mockStudentRepo;
        private readonly GradeService _service;

        public GradeServiceTests()
        {
            _mockGradeRepo = new Mock<IGenericRepository<Grade>>();
            _mockStudentRepo = new Mock<IGenericRepository<Student>>();
            _service = new GradeService(_mockGradeRepo.Object, _mockStudentRepo.Object);
        }

        [Fact]
        public async Task GetGradeByIdAsync_ReturnsGradeDto_WhenGradeExists()
        {
            // Arrange
            var grade = new Grade { Id = 1, Value = 90, LessonId = 1, StudentId = 1 };
            _mockGradeRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(grade);

            // Act
            var result = await _service.GetGradeByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(90, result.Value);
        }

        [Fact]
        public async Task GetGradeByIdAsync_ReturnsNull_WhenGradeDoesNotExist()
        {
            // Arrange
            _mockGradeRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Grade)null);

            // Act
            var result = await _service.GetGradeByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllGradesAsync_ReturnsAllGradeDtos()
        {
            // Arrange
            var grades = new List<Grade>
            {
                new Grade { Id = 1, Value = 90, LessonId = 1, StudentId = 1 },
                new Grade { Id = 2, Value = 85, LessonId = 1, StudentId = 2 }
            };
            _mockGradeRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(grades);

            // Act
            var result = await _service.GetAllGradesAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddGradeAsync_ThrowsException_WhenStudentDoesNotExist()
        {
            // Arrange
            var gradeDto = new GradeDto { Id = 1, Value = 90, LessonId = 1, StudentId = 1 };
            _mockStudentRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Student)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AddGradeAsync(gradeDto));
        }

        [Fact]
        public async Task AddGradeAsync_AddsGrade_WhenStudentExists()
        {
            // Arrange
            var gradeDto = new GradeDto { Id = 1, Value = 90, LessonId = 1, StudentId = 1 };
            var student = new Student { Id = 1, Name = "John Doe" };
            _mockStudentRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(student);

            // Act
            await _service.AddGradeAsync(gradeDto);

            // Assert
            _mockGradeRepo.Verify(repo => repo.AddAsync(It.IsAny<Grade>()), Times.Once);
        }

        [Fact]
        public async Task UpdateGradeAsync_ThrowsException_WhenGradeDoesNotExist()
        {
            // Arrange
            var gradeDto = new GradeDto { Id = 1, Value = 90, LessonId = 1, StudentId = 1 };
            _mockGradeRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Grade)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.UpdateGradeAsync(gradeDto));
        }

        [Fact]
        public async Task UpdateGradeAsync_UpdatesGrade_WhenItExists()
        {
            // Arrange
            var grade = new Grade { Id = 1, Value = 90, LessonId = 1, StudentId = 1 };
            var gradeDto = new GradeDto { Id = 1, Value = 95, LessonId = 1, StudentId = 1 };
            _mockGradeRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(grade);

            // Act
            await _service.UpdateGradeAsync(gradeDto);

            // Assert
            _mockGradeRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Grade>()), Times.Once);
        }

        [Fact]
        public async Task DeleteGradeAsync_DeletesGrade_WhenItExists()
        {
            // Act
            await _service.DeleteGradeAsync(1);

            // Assert
            _mockGradeRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
