using SchoolSchedule.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSchedule.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceDto> GetAttendanceByIdAsync(int id);
        Task<IEnumerable<AttendanceDto>> GetAllAttendancesAsync();
        Task AddAttendanceAsync(AttendanceDto attendanceDto);
        Task UpdateAttendanceAsync(AttendanceDto attendanceDto);
        Task DeleteAttendanceAsync(int id);
    }
}
