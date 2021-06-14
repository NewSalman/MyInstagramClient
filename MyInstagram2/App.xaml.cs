using MyInstagram2.Models;
using MyInstagram2.Pages;
using MyInstagram2.Services.LocalStorage;
using MyInstagram2.Services.PostServices;
using MyInstagram2.Services.UserServices;
using MyInstagram2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyInstagram2
{
    public partial class App : Application
    {
        static LocalDatabase database;
        public static UserServiceManager userManager { get; private set; }
        public static PostManager postManager { get; private set; }
        public static AutenticatedUser User { get; set; }

        public static LocalDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new LocalDatabase();
                }
                return database;
            }
        }
        
        public App()
        {
            InitializeComponent();
            User = new AutenticatedUser();
            userManager = new UserServiceManager(new UserService());
            postManager = new PostManager(new PostService());
        }

        protected async override void OnStart()
        {
            //Task.Run(Database.DeleteToken);
            AutenticatedUser user = await Database.GetTokenFromLocal();

            if (user == null)
            {
                MainPage = new LogPage();
                return;
            }

            /*User.Email = user.Email;
            User.IsAuthenticated = user.IsAuthenticated;
            User.Roles = user.Roles;
            User.Username = user.Username;*/
            User.ID = "36a0d04c-94ea-476c-9bba-052514a52cc9";
            User.Token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJAdXNlciIsImp0aSI6IjMzODM1YzRiLThjYzMtNDM4Yi05NTBmLWEzNDcwYTk0YWNkNCIsImVtYWlsIjoidXNlckBleGFtcGxlLmNvbSIsInVpZCI6IjM2YTBkMDRjLTk0ZWEtNDc2Yy05YmJhLTA1MjUxNGE1MmNjOSIsInJvbGUiOiJVc2VyIiwiZXhwIjoxNjE5ODQ5NDQ1LCJpc3MiOiJTZWN1cmVBcGkiLCJhdWQiOiJTZWN1cmVBcGlVc2VyIn0.aplCx6OUyc4LhjENtq4rszmwZC3bj-Zjhwk0FvDVyMRjQmrvn-hPzVIyM-hVVRFqKm0bkWUGJczMJWXnMgyTLw";

            MainPage = new AppShell();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}