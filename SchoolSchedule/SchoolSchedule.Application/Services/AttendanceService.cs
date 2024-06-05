using CleanArchitectureSolution.Entities;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;
using SchoolSchedule.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSchedule.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IGenericRepository<Attendance> _attendanceRepository;

        public AttendanceService(IGenericRepository<Attendance> attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<AttendanceDto> GetAttendanceByIdAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            return attendance != null ? new AttendanceDto { Id = attendance.Id, Type = attendance.Type, Date = attendance.Date } : null;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAttendancesAsync()
        {
            var attendances = await _attendanceRepository.GetAllAsync();
            return attendances.Select(attendance => new AttendanceDto { Id = attendance.Id, Type = attendance.Type, Date = attendance.Date });
        }

        public async Task AddAttendanceAsync(AttendanceDto attendanceDto)
        {
            var attendance = new Attendance { Type = attendanceDto.Type, Date = attendanceDto.Date };
            await _attendanceRepository.AddAsync(attendance);
        }

        public async Task UpdateAttendanceAsync(AttendanceDto attendanceDto)
        {
            var attendance = new Attendance { Id = attendanceDto.Id, Type = attendanceDto.Type, Date = attendanceDto.Date };
            await _attendanceRepository.UpdateAsync(attendance);
        }

        public async Task DeleteAttendanceAsync(int id)
        {
            await _attendanceRepository.DeleteAsync(id);
        }
    }
}
