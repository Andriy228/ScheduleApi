using AutoMapper;
using Entities.DtoS.Groups;
using Entities.DtoS.StudentInGroup;
using Entities.DtoS.Students;
using Entities.DtoS.Subjects;
using Entities.DtoS.Teachers;
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
    public class AdminService : IAdminService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public AdminService(IMapper mapper,ApplicationContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponce<List<GetGroupDto>>> AddGroup(AddGroupDto newGroup)
        {
            var serviceResponce = new ServiceResponce<List<GetGroupDto>>();
            _context.Groups.Add(_mapper.Map<Group>(newGroup));
            await _context.SaveChangesAsync();
            serviceResponce.Data = await _context.Groups.Select(c => _mapper.Map<GetGroupDto>(c)).ToListAsync();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetStudentInGroupDto>>> AddStudentToGroup(int StudentId, int GroupId)
        {
            var serviceResponce = new ServiceResponce<List<GetStudentInGroupDto>>();
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == GroupId);
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == StudentId);

            if (group == null || student == null) {
                serviceResponce.Success = false;
                serviceResponce.Message = "Student or group not found.";
                return serviceResponce;
            }
            if (await _context.Student_In_Groups.CountAsync(x => x.Group == group) > 5) {
                serviceResponce.Success = false;
                serviceResponce.Message = "Group must have no more than 5 students.";
                return serviceResponce;
            }
            Student_in_group group1 = new Student_in_group { Group = group, Student = student };
            if (await _context.Student_In_Groups.Where(x=>x.Student == student).FirstOrDefaultAsync() != null) {
                serviceResponce.Success = false;
                serviceResponce.Message = "Student has a group.";
                return serviceResponce;
            }
            _context.Student_In_Groups.Add(group1);
            await _context.SaveChangesAsync();
            var stInGroups = await _context.Student_In_Groups.ToListAsync();
            serviceResponce.Data = stInGroups.Select(c=>_mapper.Map<GetStudentInGroupDto>(c)).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetSubjectDto>>> AddSubject(AddSubjectDto dto)
        {
            var serviceResponce = new ServiceResponce<List<GetSubjectDto>>();
            _context.Subjects.Add(_mapper.Map<Subject>(dto));
            await _context.SaveChangesAsync();
            var subjects = await _context.Subjects.ToListAsync();
            serviceResponce.Data = subjects.Select(c => _mapper.Map<GetSubjectDto>(c)).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetGroupDto>>> GetAllGroups()
        {
            var responceService = new ServiceResponce<List<GetGroupDto>>();
            var groups = await _context.Groups.ToListAsync();
            responceService.Data = groups.Select(c => _mapper.Map<GetGroupDto>(c)).ToList();
            return responceService;
        }

        public async Task<ServiceResponce<List<GetStudentDto>>> GetAllStudents()
        {
            var serviceResponce = new ServiceResponce<List<GetStudentDto>>();
            var students = await _context.Students.ToListAsync();
            serviceResponce.Data = students.Select(c => _mapper.Map<GetStudentDto>(c)).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetSubjectDto>>> GetAllSubjects()
        {
            var serviceResponce = new ServiceResponce<List<GetSubjectDto>>();
            var subjects = await _context.Subjects.ToListAsync();
            serviceResponce.Data = subjects.Select(c => _mapper.Map<GetSubjectDto>(c)).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetTeacherDto>>> GetAllTeachers()
        {
            var serviceResponce = new ServiceResponce<List<GetTeacherDto>>();
            var teachers = await _context.Teachers.ToListAsync();
            serviceResponce.Data = teachers.Select(c => _mapper.Map<GetTeacherDto>(c)).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetStudentInGroupDto>>> GetStudentInGroups()
        {
            var serviceResponce = new ServiceResponce<List<GetStudentInGroupDto>>();
            var stInGroups = await _context.Student_In_Groups.ToListAsync();
            serviceResponce.Data = stInGroups.Select(c => _mapper.Map<GetStudentInGroupDto>(c)).ToList();
            return serviceResponce;
        }
    }
}
