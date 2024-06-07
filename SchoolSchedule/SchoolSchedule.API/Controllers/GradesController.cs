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
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetGrades()
        {
            var grades = await _gradeService.GetAllGradesAsync();
            return Ok(grades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GradeDto>> GetGrade(int id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            return Ok(grade);
        }

        [HttpPost]
        public async Task<ActionResult> AddGrade(GradeDto gradeDto)
        {
            try
            {
                await _gradeService.AddGradeAsync(gradeDto);
                return CreatedAtAction(nameof(GetGrade), new { id = gradeDto.Id }, gradeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGrade(int id, GradeDto gradeDto)
        {
            if (id != gradeDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _gradeService.UpdateGradeAsync(gradeDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGrade(int id)
        {
            await _gradeService.DeleteGradeAsync(id);
            return NoContent();
        }
    }
}
