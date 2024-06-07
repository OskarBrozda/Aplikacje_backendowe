using CleanArchitectureSolution.Entities;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;
using SchoolSchedule.Core.Entities;
using SchoolSchedule.Core.Interfaces;

namespace SchoolSchedule.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IGenericRepository<Attendance> _attendanceRepository;
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<Lesson> _lessonRepository;

        public AttendanceService(
            IGenericRepository<Attendance> attendanceRepository,
            IGenericRepository<Student> studentRepository,
            IGenericRepository<Lesson> lessonRepository)
        {
            _attendanceRepository = attendanceRepository;
            _studentRepository = studentRepository;
            _lessonRepository = lessonRepository;
        }

        public async Task<AttendanceDto> GetAttendanceByIdAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            return attendance != null ? new AttendanceDto
            {
                Id = attendance.Id,
                Type = attendance.Type,
                Date = attendance.Date,
                StudentId = attendance.StudentId,
                LessonId = attendance.LessonId
            } : null;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAttendancesAsync()
        {
            var attendances = await _attendanceRepository.GetAllAsync();
            return attendances.Select(attendance => new AttendanceDto
            {
                Id = attendance.Id,
                Type = attendance.Type,
                Date = attendance.Date,
                StudentId = attendance.StudentId,
                LessonId = attendance.LessonId
            });
        }

        public async Task AddAttendanceAsync(AttendanceDto attendanceDto)
        {
            var student = await _studentRepository.GetByIdAsync(attendanceDto.StudentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }

            var lesson = await _lessonRepository.GetByIdAsync(attendanceDto.LessonId);
            if (lesson == null)
            {
                throw new Exception("Lesson not found");
            }

            var attendance = new Attendance
            {
                Type = attendanceDto.Type,
                Date = attendanceDto.Date,
                StudentId = attendanceDto.StudentId,
                LessonId = attendanceDto.LessonId
            };

            await _attendanceRepository.AddAsync(attendance);
        }

        public async Task UpdateAttendanceAsync(AttendanceDto attendanceDto)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(attendanceDto.Id);
            if (attendance == null)
            {
                throw new Exception("Attendance not found");
            }

            attendance.Type = attendanceDto.Type;
            attendance.Date = attendanceDto.Date;
            attendance.StudentId = attendanceDto.StudentId;
            attendance.LessonId = attendanceDto.LessonId;

            await _attendanceRepository.UpdateAsync(attendance);
        }

        public async Task DeleteAttendanceAsync(int id)
        {
            await _attendanceRepository.DeleteAsync(id);
        }
    }
}
