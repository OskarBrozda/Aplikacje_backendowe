using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Interfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetLessonById(int id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return Ok(lesson);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAllLessons()
        {
            var lessons = await _lessonService.GetAllLessonsAsync();
            return Ok(lessons);
        }

        [HttpPost]
        public async Task<ActionResult> AddLesson(LessonDto lessonDto)
        {
            await _lessonService.AddLessonAsync(lessonDto);
            return CreatedAtAction(nameof(GetLessonById), new { id = lessonDto.Id }, lessonDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLesson(int id, LessonDto lessonDto)
        {
            if (id != lessonDto.Id)
            {
                return BadRequest();
            }

            await _lessonService.UpdateLessonAsync(lessonDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLesson(int id)
        {
            await _lessonService.DeleteLessonAsync(id);
            return NoContent();
        }
    }
}