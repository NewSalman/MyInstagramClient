using MyInstagram2.Models;
using MyInstagram2.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyInstagram2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostPage : ContentPage
    {
        byte[] result { get; set; }
        PostModel model { get; set; }

        public PostPage(byte[] ImageStream, PostModel postModel)
        {
            InitializeComponent();
            result = ImageStream;
            model = postModel;

            ImageSrc.Source = ImageSource.FromStream(() => new MemoryStream(ImageStream));

            BindingContext = new HomeViewModel();
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            model.Caption = Caption.Text;

            await App.postManager.PostStatus(model, result);

            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

    }
}