using MyInstagram2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyInstagram2.Services.UserServices
{
    public class UserServiceManager
    {
        readonly IUserService _userService;
        public UserServiceManager(IUserService userService)
        {
            _userService = userService;
        }

        public Task<AutenticatedUser> Login(TokenRequest model)
        {
            return _userService.Login(model);
        }
    }
}
