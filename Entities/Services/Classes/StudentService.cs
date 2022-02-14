using AutoMapper;
using Entities.DtoS.Schedules;
using Entities.Models;
using Entities.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Services.Classes
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public StudentService(IMapper mapper,ApplicationContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponce<List<GetScheduleDto>>> GetSchedule(int studentId)
        {
            var serviceResponce = new ServiceResponce<List<GetScheduleDto>>();
            var student = await _context.Student_In_Groups.Where(x => x.Student.User.Id == studentId).FirstOrDefaultAsync();
            var schedules = await _context.Schedules.Where(x => x.Group == student.Group).ToListAsync();
            serviceResponce.Data = schedules.Select(c => _mapper.Map<GetScheduleDto>(c)).ToList();
            return serviceResponce;
        }
    }
}
