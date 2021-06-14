using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyInstagram2.Models
{
    public class AutenticatedUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string ID { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
    }
}
