using Entities.DtoS.Schedules;
using Entities.DtoS.Students;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<ServiceResponce<List<GetScheduleDto>>> NoteSchedule(AddScheduleDto dto, int teacherId);
        Task<ServiceResponce<GetStudentDto>> NoteStudent(int studentId);
    }
}
