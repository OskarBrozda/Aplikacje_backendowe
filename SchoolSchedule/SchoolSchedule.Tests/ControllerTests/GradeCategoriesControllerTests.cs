using Moq;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.API.Controllers;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;
namespace SchoolSchedule.Tests
{
    public class GradeCategoriesControllerTests
    {
        private readonly Mock<IGradeCategoryService> _mockService;
        private readonly GradeCategoriesController _controller;

        public GradeCategoriesControllerTests()
        {
            _mockService = new Mock<IGradeCategoryService>();
            _controller = new GradeCategoriesController(_mockService.Object);
        }

        [Fact]
        public async Task GetGradeCategories_ReturnsOkResult_WithListOfGradeCategories()
        {
            var gradeCategories = new List<GradeCategoryDto> { new GradeCategoryDto { Id = 1, Name = "Homework" } };
            _mockService.Setup(service => service.GetAllGradeCategoriesAsync()).ReturnsAsync(gradeCategories);

            var result = await _controller.GetGradeCategories();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnGradeCategories = Assert.IsType<List<GradeCategoryDto>>(okResult.Value);
            Assert.Single(returnGradeCategories);
        }

        [Fact]
        public async Task GetGradeCategory_ReturnsOkResult_WithGradeCategory()
        {
            var gradeCategory = new GradeCategoryDto { Id = 1, Name = "Homework" };
            _mockService.Setup(service => service.GetGradeCategoryByIdAsync(1)).ReturnsAsync(gradeCategory);

            var result = await _controller.GetGradeCategory(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnGradeCategory = Assert.IsType<GradeCategoryDto>(okResult.Value);
            Assert.Equal(1, returnGradeCategory.Id);
        }

        [Fact]
        public async Task GetGradeCategory_ReturnsNotFound_WhenGradeCategoryDoesNotExist()
        {
            _mockService.Setup(service => service.GetGradeCategoryByIdAsync(1)).ReturnsAsync((GradeCategoryDto)null);

            var result = await _controller.GetGradeCategory(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddGradeCategory_ReturnsCreatedAtActionResult()
        {
            var gradeCategory = new GradeCategoryDto { Id = 1, Name = "Homework" };
            _mockService.Setup(service => service.AddGradeCategoryAsync(gradeCategory)).Returns(Task.CompletedTask);

            var result = await _controller.AddGradeCategory(gradeCategory);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnGradeCategory = Assert.IsType<GradeCategoryDto>(createdAtActionResult.Value);
            Assert.Equal(1, returnGradeCategory.Id);
        }

        [Fact]
        public async Task UpdateGradeCategory_ReturnsNoContent()
        {
            var gradeCategory = new GradeCategoryDto { Id = 1, Name = "Homework" };
            _mockService.Setup(service => service.UpdateGradeCategoryAsync(gradeCategory)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateGradeCategory(1, gradeCategory);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateGradeCategory_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var gradeCategory = new GradeCategoryDto { Id = 1, Name = "Homework" };

            var result = await _controller.UpdateGradeCategory(2, gradeCategory);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteGradeCategory_ReturnsNoContent()
        {
            _mockService.Setup(service => service.DeleteGradeCategoryAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteGradeCategory(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
