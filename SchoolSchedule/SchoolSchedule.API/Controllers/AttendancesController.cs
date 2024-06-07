using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolSchedule.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAttendances()
        {
            var attendances = await _attendanceService.GetAllAttendancesAsync();
            return Ok(attendances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDto>> GetAttendance(int id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        [HttpPost]
        public async Task<ActionResult> AddAttendance(AttendanceDto attendanceDto)
        {
            try
            {
                await _attendanceService.AddAttendanceAsync(attendanceDto);
                return CreatedAtAction(nameof(GetAttendance), new { id = attendanceDto.Id }, attendanceDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAttendance(int id, AttendanceDto attendanceDto)
        {
            if (id != attendanceDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _attendanceService.UpdateAttendanceAsync(attendanceDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttendance(int id)
        {
            await _attendanceService.DeleteAttendanceAsync(id);
            return NoContent();
        }
    }
}
