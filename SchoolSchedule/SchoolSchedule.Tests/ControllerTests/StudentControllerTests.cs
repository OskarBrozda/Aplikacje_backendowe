using Moq;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.API.Controllers;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;
namespace SchoolSchedule.Tests
{
    public class StudentsControllerTests
    {
        private readonly Mock<IStudentService> _mockService;
        private readonly StudentsController _controller;

        public StudentsControllerTests()
        {
            _mockService = new Mock<IStudentService>();
            _controller = new StudentsController(_mockService.Object);
        }

        [Fact]
        public async Task GetStudents_ReturnsOkResult_WithListOfStudents()
        {
            var students = new List<StudentDto> { new StudentDto { Id = 1, Name = "John Doe" } };
            _mockService.Setup(service => service.GetAllStudentsAsync()).ReturnsAsync(students);

            var result = await _controller.GetStudents();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnStudents = Assert.IsType<List<StudentDto>>(okResult.Value);
            Assert.Single(returnStudents);
        }

        [Fact]
        public async Task GetStudent_ReturnsOkResult_WithStudent()
        {
            var student = new StudentDto { Id = 1, Name = "John Doe" };
            _mockService.Setup(service => service.GetStudentByIdAsync(1)).ReturnsAsync(student);

            var result = await _controller.GetStudent(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnStudent = Assert.IsType<StudentDto>(okResult.Value);
            Assert.Equal(1, returnStudent.Id);
        }

        [Fact]
        public async Task GetStudent_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            _mockService.Setup(service => service.GetStudentByIdAsync(1)).ReturnsAsync((StudentDto)null);

            var result = await _controller.GetStudent(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddStudent_ReturnsCreatedAtActionResult()
        {
            var student = new StudentDto { Id = 1, Name = "John Doe" };
            _mockService.Setup(service => service.AddStudentAsync(student)).Returns(Task.CompletedTask);

            var result = await _controller.AddStudent(student);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnStudent = Assert.IsType<StudentDto>(createdAtActionResult.Value);
            Assert.Equal(1, returnStudent.Id);
        }

        [Fact]
        public async Task UpdateStudent_ReturnsNoContent()
        {
            var student = new StudentDto { Id = 1, Name = "John Doe" };
            _mockService.Setup(service => service.UpdateStudentAsync(student)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateStudent(1, student);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateStudent_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var student = new StudentDto { Id = 1, Name = "John Doe" };

            var result = await _controller.UpdateStudent(2, student);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteStudent_ReturnsNoContent()
        {
            _mockService.Setup(service => service.DeleteStudentAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteStudent(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
