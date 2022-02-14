using Entities.DtoS.Groups;
using Entities.DtoS.StudentInGroup;
using Entities.DtoS.Students;
using Entities.DtoS.Subjects;
using Entities.DtoS.Teachers;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Services.Interfaces
{
    public interface IAdminService
    {
        Task<ServiceResponce<List<GetTeacherDto>>> GetAllTeachers();
        Task<ServiceResponce<List<GetStudentDto>>> GetAllStudents();
        Task<ServiceResponce<List<GetStudentInGroupDto>>> AddStudentToGroup(int StudentId, int GroupId);
        Task<ServiceResponce<List<GetStudentInGroupDto>>> GetStudentInGroups();
        Task<ServiceResponce<List<GetGroupDto>>> GetAllGroups();
        Task<ServiceResponce<List<GetGroupDto>>> AddGroup(AddGroupDto group);
        Task<ServiceResponce<List<GetSubjectDto>>> GetAllSubjects();
        Task<ServiceResponce<List<GetSubjectDto>>> AddSubject(AddSubjectDto dto);
    }
}
