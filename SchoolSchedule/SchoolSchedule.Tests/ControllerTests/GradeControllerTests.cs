using Moq;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.API.Controllers;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;

namespace SchoolSchedule.Tests.ControllerTests
{
    public class GradesControllerTests
    {
        private readonly Mock<IGradeService> _mockService;
        private readonly GradesController _controller;

        public GradesControllerTests()
        {
            _mockService = new Mock<IGradeService>();
            _controller = new GradesController(_mockService.Object);
        }

        [Fact]
        public async Task GetGrades_ReturnsOkResult_WithListOfGrades()
        {
            var grades = new List<GradeDto> { new GradeDto { Id = 1, Value = 5 } };
            _mockService.Setup(service => service.GetAllGradesAsync()).ReturnsAsync(grades);

            var result = await _controller.GetGrades();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnGrades = Assert.IsType<List<GradeDto>>(okResult.Value);
            Assert.Single(returnGrades);
        }

        [Fact]
        public async Task GetGrade_ReturnsOkResult_WithGrade()
        {
            var grade = new GradeDto { Id = 1, Value = 5 };
            _mockService.Setup(service => service.GetGradeByIdAsync(1)).ReturnsAsync(grade);

            var result = await _controller.GetGrade(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnGrade = Assert.IsType<GradeDto>(okResult.Value);
            Assert.Equal(1, returnGrade.Id);
        }

        [Fact]
        public async Task GetGrade_ReturnsNotFound_WhenGradeDoesNotExist()
        {
            _mockService.Setup(service => service.GetGradeByIdAsync(1)).ReturnsAsync((GradeDto)null);

            var result = await _controller.GetGrade(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddGrade_ReturnsCreatedAtActionResult()
        {
            var grade = new GradeDto { Id = 1, Value = 5 };
            _mockService.Setup(service => service.AddGradeAsync(grade)).Returns(Task.CompletedTask);

            var result = await _controller.AddGrade(grade);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnGrade = Assert.IsType<GradeDto>(createdAtActionResult.Value);
            Assert.Equal(1, returnGrade.Id);
        }

        [Fact]
        public async Task UpdateGrade_ReturnsNoContent()
        {
            var grade = new GradeDto { Id = 1, Value = 5 };
            _mockService.Setup(service => service.UpdateGradeAsync(grade)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateGrade(1, grade);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateGrade_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var grade = new GradeDto { Id = 1, Value = 5 };

            var result = await _controller.UpdateGrade(2, grade);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteGrade_ReturnsNoContent()
        {
            _mockService.Setup(service => service.DeleteGradeAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteGrade(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
