using Entities.DtoS.Schedules;
using Entities.DtoS.Subjects;
using Entities.Models;
using Entities.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScheduleApi.Controllers
{
    [Authorize(Roles = "Student")]
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService service)
        {
            studentService = service;
        }

        [HttpGet("GetSchedules")]
        public async Task<ActionResult<ServiceResponce<List<GetScheduleDto>>>> GetSchedules()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await studentService.GetSchedule(id));
        }

    }
}
