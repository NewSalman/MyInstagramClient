using MyInstagram2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyInstagram2.Services.PostServices
{
    public interface IPostService
    {
        Task<PostModel> PostStatus(PostModel model, byte[] arr);

        Task<ObservableCollection<PostCard>> UserHome();

        Task<ObservableCollection<PostCard>> ProfileSource(string username);

#if DEBUG
        Task<List<HomeFeedModel>> Test();
#endif
    }
}
