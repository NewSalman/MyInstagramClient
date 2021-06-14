using MyInstagram2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyInstagram2.Services.PostServices
{ 
    public class PostManager
    {
        private readonly IPostService _postService;

        public PostManager(IPostService postService)
        {
            _postService = postService;
        }

        public Task<PostModel> PostStatus(PostModel model, byte[] arr)
        {
            return _postService.PostStatus(model, arr);
        }

        public Task<ObservableCollection<PostCard>> Home()
        {
            return _postService.UserHome();
        }

        public Task<ObservableCollection<PostCard>> Profile(string username)
        {
            return _postService.ProfileSource(username);
        }

#if DEBUG
        public Task<List<HomeFeedModel>> Test()
        {
            return _postService.Test();
        }
#endif
    }
}
