using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Interfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDto>> GetAttendanceById(int id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return Ok(attendance);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAllAttendances()
        {
            var attendances = await _attendanceService.GetAllAttendancesAsync();
            return Ok(attendances);
        }

        [HttpPost]
        public async Task<ActionResult> AddAttendance(AttendanceDto attendanceDto)
        {
            await _attendanceService.AddAttendanceAsync(attendanceDto);
            return CreatedAtAction(nameof(GetAttendanceById), new { id = attendanceDto.Id }, attendanceDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAttendance(int id, AttendanceDto attendanceDto)
        {
            if (id != attendanceDto.Id)
            {
                return BadRequest();
            }

            await _attendanceService.UpdateAttendanceAsync(attendanceDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttendance(int id)
        {
            await _attendanceService.DeleteAttendanceAsync(id);
            return NoContent();
        }
    }
}