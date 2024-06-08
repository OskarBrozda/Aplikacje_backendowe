using Moq;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Services;
using SchoolSchedule.Core.Interfaces;
using CleanArchitectureSolution.Entities;

namespace SchoolSchedule.Tests.ServiceTests
{
    public class StudentServiceTests
    {
        private readonly Mock<IGenericRepository<Student>> _mockStudentRepo;
        private readonly StudentService _service;

        public StudentServiceTests()
        {
            _mockStudentRepo = new Mock<IGenericRepository<Student>>();
            _service = new StudentService(_mockStudentRepo.Object);
        }

        [Fact]
        public async Task GetStudentByIdAsync_ReturnsStudentDto_WhenStudentExists()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "John Doe" };
            _mockStudentRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(student);

            // Act
            var result = await _service.GetStudentByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public async Task GetStudentByIdAsync_ReturnsNull_WhenStudentDoesNotExist()
        {
            // Arrange
            _mockStudentRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Student)null);

            // Act
            var result = await _service.GetStudentByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllStudentsAsync_ReturnsAllStudentDtos()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "John Doe" },
                new Student { Id = 2, Name = "Jane Doe" }
            };
            _mockStudentRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(students);

            // Act
            var result = await _service.GetAllStudentsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddStudentAsync_AddsStudent()
        {
            // Arrange
            var studentDto = new StudentDto { Id = 1, Name = "John Doe" };

            // Act
            await _service.AddStudentAsync(studentDto);

            // Assert
            _mockStudentRepo.Verify(repo => repo.AddAsync(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task UpdateStudentAsync_ThrowsException_WhenStudentDoesNotExist()
        {
            // Arrange
            var studentDto = new StudentDto { Id = 1, Name = "John Doe" };
            _mockStudentRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Student)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.UpdateStudentAsync(studentDto));
        }

        [Fact]
        public async Task UpdateStudentAsync_UpdatesStudent_WhenItExists()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "John Doe" };
            var studentDto = new StudentDto { Id = 1, Name = "John Updated Doe" };
            _mockStudentRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(student);

            // Act
            await _service.UpdateStudentAsync(studentDto);

            // Assert
            _mockStudentRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task DeleteStudentAsync_DeletesStudent_WhenItExists()
        {
            // Act
            await _service.DeleteStudentAsync(1);

            // Assert
            _mockStudentRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
