using SchoolSchedule.Application.DTOs;

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
