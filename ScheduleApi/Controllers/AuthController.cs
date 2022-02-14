using Entities.DtoS.Users;
using Entities.Models;
using Entities.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ScheduleApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        public AuthController(IAuthRepository repository)
        {
            authRepository = repository;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponce<int>>> Register(UserRegisterDto request) {
            var responce = await authRepository.Register(
                new User { Username = request.Username}, request.Password);
            if (!responce.Success) {
                return BadRequest(responce);
            }
            return Ok(responce);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponce<string>>> Login(UserLoginDto request)
        {
            var responce = await authRepository.Login(request.Username, request.Password);
            if (!responce.Success)
            {
                return BadRequest(responce);
            }
            return Ok(responce);
        }
    }
}
