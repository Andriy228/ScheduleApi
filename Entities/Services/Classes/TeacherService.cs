using AutoMapper;
using Entities.DtoS.Schedules;
using Entities.DtoS.Students;
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
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public TeacherService(IMapper mapper, ApplicationContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponce<List<GetScheduleDto>>> NoteSchedule(AddScheduleDto dto, int teacherId)
        {
            var serviceResponce = new ServiceResponce<List<GetScheduleDto>>();
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.User.Id == teacherId);
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == dto.GroupId);
            var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == dto.SubjectId);
            if (teacher == null || group == null || subject == null) {
                serviceResponce.Success = false;
                serviceResponce.Message = "Teacher or group or subject don't exists.";
                return serviceResponce;
            }
            _context.Schedules.Add(new Schedule { Teacher = teacher, Group = group, Subject = subject, Date = dto.Date });
            await _context.SaveChangesAsync();
            serviceResponce.Data = _context.Schedules.Select(c => _mapper.Map<GetScheduleDto>(c)).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetStudentDto>> NoteStudent(int studentId)
        {
            var serviceResponce = new ServiceResponce<GetStudentDto>();
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == studentId);
            if (student == null) {
                serviceResponce.Success = false;
                serviceResponce.Message = "Student not found.";
                return serviceResponce;
            }
            student.MissC++;
            await _context.SaveChangesAsync();
            serviceResponce.Data = _mapper.Map<GetStudentDto>(student);
            return serviceResponce;
        }
    }
}
