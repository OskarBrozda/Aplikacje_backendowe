using Moq;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.API.Controllers;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;

namespace SchoolSchedule.Tests
{
    public class LessonsControllerTests
    {
        private readonly Mock<ILessonService> _mockService;
        private readonly LessonsController _controller;

        public LessonsControllerTests()
        {
            _mockService = new Mock<ILessonService>();
            _controller = new LessonsController(_mockService.Object);
        }

        [Fact]
        public async Task GetLessonById_ReturnsOkResult_WithLesson()
        {
            var lesson = new LessonDto { Id = 1, Title = "Math" };
            _mockService.Setup(service => service.GetLessonByIdAsync(1)).ReturnsAsync(lesson);

            var result = await _controller.GetLessonById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnLesson = Assert.IsType<LessonDto>(okResult.Value);
            Assert.Equal(1, returnLesson.Id);
        }

        [Fact]
        public async Task GetLessonById_ReturnsNotFound_WhenLessonDoesNotExist()
        {
            _mockService.Setup(service => service.GetLessonByIdAsync(1)).ReturnsAsync((LessonDto)null);

            var result = await _controller.GetLessonById(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAllLessons_ReturnsOkResult_WithListOfLessons()
        {
            var lessons = new List<LessonDto> { new LessonDto { Id = 1, Title = "Math" } };
            _mockService.Setup(service => service.GetAllLessonsAsync()).ReturnsAsync(lessons);

            var result = await _controller.GetAllLessons();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnLessons = Assert.IsType<List<LessonDto>>(okResult.Value);
            Assert.Single(returnLessons);
        }

        [Fact]
        public async Task AddLesson_ReturnsCreatedAtActionResult()
        {
            var lesson = new LessonDto { Id = 1, Title = "Math" };
            _mockService.Setup(service => service.AddLessonAsync(lesson)).Returns(Task.CompletedTask);

            var result = await _controller.AddLesson(lesson);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnLesson = Assert.IsType<LessonDto>(createdAtActionResult.Value);
            Assert.Equal(1, returnLesson.Id);
        }

        [Fact]
        public async Task UpdateLesson_ReturnsNoContent()
        {
            var lesson = new LessonDto { Id = 1, Title = "Math" };
            _mockService.Setup(service => service.UpdateLessonAsync(lesson)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateLesson(1, lesson);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateLesson_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var lesson = new LessonDto { Id = 1, Title = "Math" };

            var result = await _controller.UpdateLesson(2, lesson);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteLesson_ReturnsNoContent()
        {
            _mockService.Setup(service => service.DeleteLessonAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteLesson(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
