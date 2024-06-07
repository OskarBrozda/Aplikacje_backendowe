using CleanArchitectureSolution.Entities;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Interfaces;
using SchoolSchedule.Core.Interfaces;

namespace SchoolSchedule.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;

        public StudentService(IGenericRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student != null ? new StudentDto { Id = student.Id, Name = student.Name } : null;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Select(student => new StudentDto { Id = student.Id, Name = student.Name });
        }

        public async Task AddStudentAsync(StudentDto studentDto)
        {
            var student = new Student { Name = studentDto.Name };
            await _studentRepository.AddAsync(student);
        }

        public async Task UpdateStudentAsync(StudentDto studentDto)
        {
            var student = await _studentRepository.GetByIdAsync(studentDto.Id);
            if (student == null)
            {
                throw new Exception("Student not found");
            }

            student.Name = studentDto.Name;
            await _studentRepository.UpdateAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentRepository.DeleteAsync(id);
        }
    }
}
