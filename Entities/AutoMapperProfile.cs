using AutoMapper;
using Entities.DtoS.Groups;
using Entities.DtoS.Schedules;
using Entities.DtoS.StudentInGroup;
using Entities.DtoS.Students;
using Entities.DtoS.Subjects;
using Entities.DtoS.Teachers;
using Entities.Models;

namespace Entities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Group, GetGroupDto>();
            CreateMap<AddGroupDto, Group>();
            CreateMap<Subject, GetSubjectDto>();
            CreateMap<AddSubjectDto, Subject>();
            CreateMap<AddScheduleDto, Schedule>();
            CreateMap<Schedule, GetScheduleDto>();
            CreateMap<Student, GetStudentDto>();
            CreateMap<Teacher, GetTeacherDto>();
            CreateMap<Student_in_group, GetStudentInGroupDto>();
        }
    }
}
