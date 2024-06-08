using Moq;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.API.Controllers;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;

namespace SchoolSchedule.Tests.ControllerTests
{
    public class AttendancesControllerTests
    {
        private readonly Mock<IAttendanceService> _mockService;
        private readonly AttendancesController _controller;

        public AttendancesControllerTests()
        {
            _mockService = new Mock<IAttendanceService>();
            _controller = new AttendancesController(_mockService.Object);
        }

        [Fact]
        public async Task GetAttendances_ReturnsOkResult_WithListOfAttendances()
        {
            var attendances = new List<AttendanceDto> { new AttendanceDto { Id = 1, StudentId = 1 } };
            _mockService.Setup(service => service.GetAllAttendancesAsync()).ReturnsAsync(attendances);

            var result = await _controller.GetAttendances();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAttendances = Assert.IsType<List<AttendanceDto>>(okResult.Value);
            Assert.Single(returnAttendances);
        }

        [Fact]
        public async Task GetAttendance_ReturnsOkResult_WithAttendance()
        {
            var attendance = new AttendanceDto { Id = 1, StudentId = 1 };
            _mockService.Setup(service => service.GetAttendanceByIdAsync(1)).ReturnsAsync(attendance);

            var result = await _controller.GetAttendance(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAttendance = Assert.IsType<AttendanceDto>(okResult.Value);
            Assert.Equal(1, returnAttendance.Id);
        }

        [Fact]
        public async Task GetAttendance_ReturnsNotFound_WhenAttendanceDoesNotExist()
        {
            _mockService.Setup(service => service.GetAttendanceByIdAsync(1)).ReturnsAsync((AttendanceDto)null);

            var result = await _controller.GetAttendance(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddAttendance_ReturnsCreatedAtActionResult()
        {
            var attendance = new AttendanceDto { Id = 1, StudentId = 1 };
            _mockService.Setup(service => service.AddAttendanceAsync(attendance)).Returns(Task.CompletedTask);

            var result = await _controller.AddAttendance(attendance);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnAttendance = Assert.IsType<AttendanceDto>(createdAtActionResult.Value);
            Assert.Equal(1, returnAttendance.Id);
        }

        [Fact]
        public async Task UpdateAttendance_ReturnsNoContent()
        {
            var attendance = new AttendanceDto { Id = 1, StudentId = 1 };
            _mockService.Setup(service => service.UpdateAttendanceAsync(attendance)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateAttendance(1, attendance);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateAttendance_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var attendance = new AttendanceDto { Id = 1, StudentId = 1 };

            var result = await _controller.UpdateAttendance(2, attendance);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteAttendance_ReturnsNoContent()
        {
            _mockService.Setup(service => service.DeleteAttendanceAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteAttendance(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
