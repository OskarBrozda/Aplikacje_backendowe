using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Interfaces;
using SchoolSchedule.Application.DTOs;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<GradeDto>> GetGradeById(int id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            return Ok(grade);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetAllGrades()
        {
            var grades = await _gradeService.GetAllGradesAsync();
            return Ok(grades);
        }

        [HttpPost]
        public async Task<ActionResult> AddGrade(GradeDto gradeDto)
        {
            await _gradeService.AddGradeAsync(gradeDto);
            return CreatedAtAction(nameof(GetGradeById), new { id = gradeDto.Id }, gradeDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGrade(int id, GradeDto gradeDto)
        {
            if (id != gradeDto.Id)
            {
                return BadRequest();
            }

            await _gradeService.UpdateGradeAsync(gradeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGrade(int id)
        {
            await _gradeService.DeleteGradeAsync(id);
            return NoContent();
        }
    }
}