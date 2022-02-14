using Entities.Models;
using Entities.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Services.Classes
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationContext applicationContext;
        private readonly IConfiguration _configuration;
        public AuthRepository(ApplicationContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            applicationContext = context;
        }
        public async Task<ServiceResponce<string>> Login(string username, string password)
        {
            var responce = new ServiceResponce<string>();
            var user = await applicationContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                responce.Success = false;
                responce.Message = "User Not Found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                responce.Success = false;
                responce.Message = "Wrong Password.";
            }
            else {
                responce.Data = CreateToken(user);
            }
            return responce;
        }

        public async Task<ServiceResponce<int>> Register(User user, string password)
        {
            var responce = new ServiceResponce<int>();
            if (await UserExists(user.Username)) {
                responce.Success = false;
                responce.Message = "User already exists.";
                return responce;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            applicationContext.Users.Add(user);
            if (user.Role == null) { applicationContext.Students.Add(new Student { Name = user.Username, User = user }); }
            await applicationContext.SaveChangesAsync();
            responce.Data = user.Id;
            return responce;
        }
        public async Task<bool> UserExists(string username)
        {
            if (await applicationContext.Users.AnyAsync(x => x.Username.ToLower().Equals(username.ToLower()))) {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != passwordHash[i]) {
                        return false;
                    }
                }
                return true;
            }
        
        }
        private string CreateToken(User user) {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
