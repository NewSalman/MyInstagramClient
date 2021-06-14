using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyInstagram2.Models
{
    public class PostCard
    {
        public string ID { get; set; }
        public string PostedAt { get; set; }
        public string Username { get; set; }
        public string UserID { get; set; }
        public ImageSource FileSource { get; set; }
        public string Caption { get; set; }
        public int Likes { get; set; }
        public int CommentsCount { get; set; }
        public int Share { get; set; }
    }
}
