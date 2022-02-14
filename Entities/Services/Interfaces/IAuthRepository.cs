using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Services.Interfaces
{
    public interface IAuthRepository
    {
        Task<ServiceResponce<int>> Register(User user, string password);
        Task<ServiceResponce<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
