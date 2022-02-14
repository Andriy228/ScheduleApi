using Entities;
using Entities.DtoS.Schedules;
using Entities.DtoS.Students;
using Entities.Models;
using Entities.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScheduleApi.Controllers
{
    [Authorize(Roles = "Teacher")]
    [ApiController]
    [Route("[controller]")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService teacherService;
        public TeacherController(ITeacherService service)
        {
            teacherService = service;
        }
        [HttpPost("NoteSchedule")]
        public async Task<ActionResult<ServiceResponce<List<GetScheduleDto>>>> NoteSchedule(AddScheduleDto dto)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await teacherService.NoteSchedule(dto,id));
        }

            [HttpPost("NoteStudent/{id}")]
        public async Task<ActionResult<ServiceResponce<GetStudentDto>>> NoteStudent(int id) {
            var serviceResponce = await teacherService.NoteStudent(id);
            if (serviceResponce.Data == null) {
                return NotFound(serviceResponce);
            }
            return serviceResponce;
        }
    }
}
