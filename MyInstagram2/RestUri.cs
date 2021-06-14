using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MyInstagram2
{
    public class RestUri
    {
        public static string uri = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:5001{0}" : "https://localhost:5001{0}";
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Token.db3";
    
    }
}
