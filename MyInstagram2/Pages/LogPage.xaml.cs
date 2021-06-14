using MyInstagram2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyInstagram2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogPage : ContentPage
    {
        public LogPage()
        {
            InitializeComponent();
        }

        async void OnLoginClicked(object sender, EventArgs args)
        {

            TokenRequest model = new TokenRequest()
            {
                Email = "user@example.com",
                Password = "rPpnZoxD744a5QJ"
            };

            var user = await App.userManager.Login(model);

            if (user.IsAuthenticated == false)
            {
                ShowMessage(user.Message);
                return;
            }

            var User = App.User;

            User.Email = user.Email;
            User.IsAuthenticated = user.IsAuthenticated;
            User.Roles = user.Roles;
            User.Token = user.Token;
            User.Username = user.Username;

            Application.Current.MainPage = new AppShell();
        }

        void ShowMessage(string message)
        {
            Application.Current.MainPage.DisplayAlert("Authentication Error", message, "ok");
        }
    }
}