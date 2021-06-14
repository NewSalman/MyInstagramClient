using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Widget;
using MyInstagram2.Models;
using MyInstagram2.Pages;
using MyInstagram2.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyInstagram2.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        AutenticatedUser _user = new AutenticatedUser();
        ObservableCollection<PostCard> profilePost { get; set; }
        ObservableCollection<PostCard> posts { get; set; }
        private bool isRefreshing { get; set; }

        //Binding Property

        //Command
        public ICommand ToPostPage { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        //Variable
        public ObservableCollection<PostCard> Post
        {
            get
            {
                return posts;
            }

            set
            { 
                if(posts != value)
                {
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<PostCard> ProfilePost
        {
            get
            {
                return profilePost;
            }

            set
            {
                if (profilePost != value)
                {
                    OnPropertyChanged();
                }
            }
        }

        public string Message { get; private set; }
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            private set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        

        // User Variable
        public string Username { get => "@user"; }
        public ImageSource ProfilePicture { get => ImageSource.FromUri(new Uri("http://10.0.2.2:5000/images/ProfilePicture.jpg")); }



        public HomeViewModel()
        {
            /*_user.Email = user.Email;
            _user.Token = user.Token;
            _user.Username = user.Username;
            _user.IsAuthenticated = user.IsAuthenticated;
            _user.Roles = user.Roles;
            _user.Message = user.Message;*/


            ToPostPage = new Command(async () => await PostAsync());
            RefreshCommand = new Command(async () =>
            {
                await Init();
                isRefreshing = false;
                OnPropertyChanged(nameof(Post));
                OnPropertyChanged(nameof(IsRefreshing));
            });
        }

        private async Task NavigateTo(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private async Task PostAsync()
        {
            await Permission.CheckPermission();

            try
            {
                var FileResult = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
                {
                    Title = "Post",

                });

                if(FileResult == null)
                {
                    return;
                }

                using(Stream s = await FileResult.OpenReadAsync())
                using (MemoryStream ms = new MemoryStream())
                {
                    await s.CopyToAsync(ms);

                    var x = new PostModel()
                    {
                        Sender = "@user",
                        MimeType = new FileInfo(FileResult.FileName).Extension,
                        Filename = FileResult.FileName,
                        Message = FileResult.ContentType
                    };

                    await NavigateTo(new PostPage(ms.ToArray(), x));
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "ok");
                return;
            }
        }

        public override async Task Init()
        {
            try
            {
                posts = new ObservableCollection<PostCard>();
                profilePost = new ObservableCollection<PostCard>();

                var Posts = await App.postManager.Home();

                if (Posts == null)
                {
                    posts.Add(new PostCard()
                    {
                        Caption = "No post Found"
                    });
                    return;
                }

                foreach (var item in Posts)
                {
                    if(item.UserID == App.User.ID)
                    {
                        profilePost.Add(item);
                    }
                    posts.Add(item);
                }

            }
            catch(TaskCanceledException)
            {
                await Application.Current.MainPage.DisplayAlert("Error","Please Check your internet connection", "ok");
                posts.Add(new PostCard()
                {
                    Caption = "No post Found"
                });
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("", e.Message, "ok");
                posts.Add(new PostCard()
                {
                    Caption = "No post Found"
                });
            }
        }

    }
}
