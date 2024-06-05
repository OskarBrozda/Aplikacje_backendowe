using SchoolSchedule.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSchedule.Application.Interfaces
{
    public interface IGradeService
    {
        Task<GradeDto> GetGradeByIdAsync(int id);
        Task<IEnumerable<GradeDto>> GetAllGradesAsync();
        Task AddGradeAsync(GradeDto gradeDto);
        Task UpdateGradeAsync(GradeDto gradeDto);
        Task DeleteGradeAsync(int id);
    }
}
