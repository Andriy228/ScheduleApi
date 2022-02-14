using Entities.DtoS.Schedules;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities.Services.Interfaces
{
    public interface IStudentService
    {
        Task<ServiceResponce<List<GetScheduleDto>>> GetSchedule(int studentId);
    }
}
