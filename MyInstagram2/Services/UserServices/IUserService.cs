using MyInstagram2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyInstagram2.Services.UserServices
{
    public interface IUserService
    {
        Task<AutenticatedUser> Login(TokenRequest model);
    }
}
