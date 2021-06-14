using Microsoft.AspNetCore.Http;
using MyInstagram2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyInstagram2.Services.PostServices
{
    public class PostService : BaseHttpClient, IPostService
    {
        public PostService()
        {
           
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<PostModel> PostStatus(PostModel model, byte[] arr)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.User.Token);
            string PostUri = string.Format(uri, "/api/mobile/user/post");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, PostUri);
            requestMessage.Headers.Add("Accept", "multipart/form-data");
            requestMessage.Content = await MultiPartFile(arr, model);

            PostModel postModel = new PostModel();
            HttpResponseMessage response;
            try
            {
                
                response = await _client.SendAsync(requestMessage);

                Console.WriteLine(response.StatusCode);
                if (!response.IsSuccessStatusCode)
                {
                    postModel.Succeded = false;
                    postModel.Message = response.StatusCode.ToString();
                    return postModel;
                }
                postModel.Succeded = true;
                return postModel;
            } catch(DirectoryNotFoundException)
            {
                postModel.Succeded = false;
                postModel.Message = "Directory not founded";
                return postModel;
            } catch(FileNotFoundException)
            {
                postModel.Succeded = false;
                postModel.Message = "Files empty or missing";
                return postModel;
            } catch(HttpRequestException)
            {
                postModel.Succeded = false;
                postModel.Message = "Network time out";
                return postModel;
            } catch(Exception)
            {
                postModel.Message = "Please contact costumer services";
                postModel.Succeded = false;
                return postModel;
            }

        }

        private async Task<PostCard> ConvertToPostCard(HomeFeedModel item)
        {
            FileInfo info = new FileInfo(item.Filename);

            string uri = $"http://10.0.2.2:5000/{((info.Extension == ".jpg") ? "images" : "videos")}/{item.Filename}";
            string format = "d, MMM yyyy, h:mm tt";

            return await Task.Run(() =>
            {
                return new PostCard()
                {
                    ID = item.ID,
                    PostedAt = $"Posted at {item.PostedAt.ToString(format)}",
                    Username = item.Username,
                    Caption = item.Caption,
                    UserID = item.UserID,
                    FileSource = ImageSource.FromUri(new Uri(uri)),
                    Likes = item.Likes,
                    CommentsCount = item.CommentsCount,
                    Share = item.Share
                };
            });
        }

        public async Task<ObservableCollection<PostCard>> UserHome()
        {
            string PostUri = string.Format(uri, "/api/mobile/user/home");
            ObservableCollection<PostCard> posts = new ObservableCollection<PostCard>();

            var result = await makeRequest<ObservableCollection<HomeFeedModel>>(HttpMethod.Get, new Uri(PostUri), null, null);
            List<Task<PostCard>> TaskPost = new List<Task<PostCard>>();

            foreach (var item in result)
            {
                FileInfo info = new FileInfo(item.Filename);
                if (info.Extension == ".mp4") continue;
                TaskPost.Add(ConvertToPostCard(item));
            }

            PostCard[] FromResult = await Task.WhenAll(TaskPost);

            foreach (var item in FromResult)
            {
                posts.Add(item);
            }
            return posts;

        }

        private async Task<MultipartFormDataContent> MultiPartFile(byte[] arr, PostModel model)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();

            var contentPost = JsonConvert.SerializeObject(model);

            ByteArrayContent ByteContent = new ByteArrayContent(arr);
            ByteContent.Headers.ContentType = MediaTypeHeaderValue.Parse(model.Message);

            StringContent PostUserModel = new StringContent(contentPost, Encoding.UTF8, "application/json");


            content.Add(PostUserModel, "DataUser", model.Sender);
            content.Add(ByteContent, "FileUser", model.Filename);
            return content;
        }

        public async Task<ObservableCollection<PostCard>> ProfileSource(string username)
        {
            Uri uri = new Uri(string.Format(RestUri.uri, $"user/profile/{username}"));
            ObservableCollection<PostCard> posts = new ObservableCollection<PostCard>();
            List<Task<PostCard>> TaskPost = new List<Task<PostCard>>();

            var result = await makeRequest<ObservableCollection<HomeFeedModel>>(HttpMethod.Get, uri, null, null);

            foreach(var item in result)
            {
                TaskPost.Add(ConvertToPostCard(item));
            }

            PostCard[] FromResult = await Task.WhenAll(TaskPost);

            foreach (var item in FromResult)
            {
                posts.Add(item);
            }
            return posts;

        }

#if DEBUG
        public async Task<List<HomeFeedModel>> Test()
        {
            string PostUri = string.Format(uri, "/api/mobile/user/home");
            return await makeRequest<List<HomeFeedModel>>(HttpMethod.Get, new Uri(PostUri), null, null);
        }
#endif
    }
}

