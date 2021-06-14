using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace MyInstagram2.Models
{
    public class PostModel
    {
        public string Sender { get; set; }
        public DateTime PostedAt { get; } = DateTime.Now;
        public string Filename { get; set; }
        public string MimeType { get; set; }
        public string Caption { get; set; }
        public bool Succeded { get; set; }
        public string Message { get; set; }
    }
}
