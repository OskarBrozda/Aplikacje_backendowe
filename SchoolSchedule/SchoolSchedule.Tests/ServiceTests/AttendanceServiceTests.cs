using Moq;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Services;
using SchoolSchedule.Core.Entities;
using SchoolSchedule.Core.Interfaces;
using CleanArchitectureSolution.Entities;

namespace SchoolSchedule.Tests.ServiceTests
{
    public class AttendanceServiceTests
    {
        private readonly Mock<IGenericRepository<Attendance>> _mockAttendanceRepo;
        private readonly Mock<IGenericRepository<Student>> _mockStudentRepo;
        private readonly Mock<IGenericRepository<Lesson>> _mockLessonRepo;
        private readonly AttendanceService _service;

        public AttendanceServiceTests()
        {
            _mockAttendanceRepo = new Mock<IGenericRepository<Attendance>>();
            _mockStudentRepo = new Mock<IGenericRepository<Student>>();
            _mockLessonRepo = new Mock<IGenericRepository<Lesson>>();
            _service = new AttendanceService(_mockAttendanceRepo.Object, _mockStudentRepo.Object, _mockLessonRepo.Object);
        }

        [Fact]
        public async Task GetAttendanceByIdAsync_ReturnsAttendanceDto_WhenAttendanceExists()
        {
            // Arrange
            var attendance = new Attendance { Id = 1, Type = "Present", Date = DateTime.Now, StudentId = 1, LessonId = 1 };
            _mockAttendanceRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(attendance);

            // Act
            var result = await _service.GetAttendanceByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Present", result.Type);
        }

        [Fact]
        public async Task GetAttendanceByIdAsync_ReturnsNull_WhenAttendanceDoesNotExist()
        {
            // Arrange
            _mockAttendanceRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Attendance)null);

            // Act
            var result = await _service.GetAttendanceByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAttendancesAsync_ReturnsAllAttendanceDtos()
        {
            // Arrange
            var attendances = new List<Attendance>
            {
                new Attendance { Id = 1, Type = "Present", Date = DateTime.Now, StudentId = 1, LessonId = 1 },
                new Attendance { Id = 2, Type = "Absent", Date = DateTime.Now, StudentId = 2, LessonId = 2 }
            };
            _mockAttendanceRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(attendances);

            // Act
            var result = await _service.GetAllAttendancesAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddAttendanceAsync_ThrowsException_WhenStudentDoesNotExist()
        {
            // Arrange
            var attendanceDto = new AttendanceDto { Id = 1, Type = "Present", Date = DateTime.Now, StudentId = 1, LessonId = 1 };
            _mockStudentRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Student)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AddAttendanceAsync(attendanceDto));
        }

        [Fact]
        public async Task AddAttendanceAsync_ThrowsException_WhenLessonDoesNotExist()
        {
            // Arrange
            var attendanceDto = new AttendanceDto { Id = 1, Type = "Present", Date = DateTime.Now, StudentId = 1, LessonId = 1 };
            var student = new Student { Id = 1, Name = "John Doe" };
            _mockStudentRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(student);
            _mockLessonRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Lesson)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AddAttendanceAsync(attendanceDto));
        }

        [Fact]
        public async Task AddAttendanceAsync_AddsAttendance_WhenStudentAndLessonExist()
        {
            // Arrange
            var attendanceDto = new AttendanceDto { Id = 1, Type = "Present", Date = DateTime.Now, StudentId = 1, LessonId = 1 };
            var student = new Student { Id = 1, Name = "John Doe" };
            var lesson = new Lesson { Id = 1, Title = "Math" };
            _mockStudentRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(student);
            _mockLessonRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(lesson);

            // Act
            await _service.AddAttendanceAsync(attendanceDto);

            // Assert
            _mockAttendanceRepo.Verify(repo => repo.AddAsync(It.IsAny<Attendance>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAttendanceAsync_ThrowsException_WhenAttendanceDoesNotExist()
        {
            // Arrange
            var attendanceDto = new AttendanceDto { Id = 1, Type = "Present", Date = DateTime.Now, StudentId = 1, LessonId = 1 };
            _mockAttendanceRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Attendance)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.UpdateAttendanceAsync(attendanceDto));
        }

        [Fact]
        public async Task UpdateAttendanceAsync_UpdatesAttendance_WhenItExists()
        {
            // Arrange
            var attendance = new Attendance { Id = 1, Type = "Present", Date = DateTime.Now, StudentId = 1, LessonId = 1 };
            var attendanceDto = new AttendanceDto { Id = 1, Type = "Absent", Date = DateTime.Now, StudentId = 1, LessonId = 1 };
            _mockAttendanceRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(attendance);

            // Act
            await _service.UpdateAttendanceAsync(attendanceDto);

            // Assert
            _mockAttendanceRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Attendance>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAttendanceAsync_DeletesAttendance_WhenItExists()
        {
            // Act
            await _service.DeleteAttendanceAsync(1);

            // Assert
            _mockAttendanceRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
