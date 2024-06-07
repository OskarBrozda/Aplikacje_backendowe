using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolSchedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeCategoriesController : ControllerBase
    {
        private readonly IGradeCategoryService _gradeCategoryService;

        public GradeCategoriesController(IGradeCategoryService gradeCategoryService)
        {
            _gradeCategoryService = gradeCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeCategoryDto>>> GetGradeCategories()
        {
            var gradeCategories = await _gradeCategoryService.GetAllGradeCategoriesAsync();
            return Ok(gradeCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GradeCategoryDto>> GetGradeCategory(int id)
        {
            var gradeCategory = await _gradeCategoryService.GetGradeCategoryByIdAsync(id);
            if (gradeCategory == null)
            {
                return NotFound();
            }

            return Ok(gradeCategory);
        }

        [HttpPost]
        public async Task<ActionResult> AddGradeCategory(GradeCategoryDto gradeCategoryDto)
        {
            try
            {
                await _gradeCategoryService.AddGradeCategoryAsync(gradeCategoryDto);
                return CreatedAtAction(nameof(GetGradeCategory), new { id = gradeCategoryDto.Id }, gradeCategoryDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGradeCategory(int id, GradeCategoryDto gradeCategoryDto)
        {
            if (id != gradeCategoryDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _gradeCategoryService.UpdateGradeCategoryAsync(gradeCategoryDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGradeCategory(int id)
        {
            await _gradeCategoryService.DeleteGradeCategoryAsync(id);
            return NoContent();
        }
    }
}
