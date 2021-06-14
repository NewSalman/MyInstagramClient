using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyInstagram2.Models
{
    public class DatabaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public bool IsAuthenticated { get; set; }


        [DataType(DataType.Date)]
        public DateTime LoginAt { get; set; }
    }
}
