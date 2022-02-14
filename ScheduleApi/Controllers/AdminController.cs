using Entities;
using Entities.DtoS.Groups;
using Entities.DtoS.StudentInGroup;
using Entities.DtoS.Students;
using Entities.DtoS.Subjects;
using Entities.DtoS.Teachers;
using Entities.Models;
using Entities.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        public AdminController(IAdminService service)
        {
            adminService = service;
        }

        [HttpGet("GetAllStudents")]
        public async Task<ActionResult<ServiceResponce<List<GetStudentDto>>>> GetAllStudents() {
            return Ok(await adminService.GetAllStudents());
        }
        [HttpGet("GetAllTeachers")]
        public async Task<ActionResult<ServiceResponce<List<GetTeacherDto>>>> GetAllTeachers()
        {
            return Ok(await adminService.GetAllTeachers());
        }
        [HttpGet("GetAllGroups")]
        public async Task<ActionResult<ServiceResponce<List<GetGroupDto>>>> GetAllGroups()
        {
            return Ok(await adminService.GetAllGroups());
        }
        [HttpGet("GetStudentInGroup")]
        public async Task<ActionResult<ServiceResponce<List<GetStudentInGroupDto>>>> GetStudentInGroup()
        {
            return Ok(await adminService.GetStudentInGroups());
        }
        [HttpPost("AddGroup")]
        public async Task<ActionResult<ServiceResponce<List<GetGroupDto>>>> AddGroup(AddGroupDto dto) {
            return Ok(await adminService.AddGroup(dto));
        }

        [HttpPost("AddStudentToGroup")]
        public async Task<ActionResult<ServiceResponce<List<GetStudentInGroupDto>>>> AddStudentToGroup(int studentId,int groupId)
        {
            return Ok(await adminService.AddStudentToGroup(studentId,groupId));
        }
        [HttpPost("AddSubject")]
        public async Task<ActionResult<ServiceResponce<List<GetSubjectDto>>>> AddSubject(AddSubjectDto dto) {
            return Ok(await adminService.AddSubject(dto));
        }
        [HttpPost("GetAllSubject")]
        public async Task<ActionResult<ServiceResponce<List<GetSubjectDto>>>> GetAllSubject()
        {
            return Ok(await adminService.GetAllSubjects());
        }
    }
}
