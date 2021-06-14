using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyInstagram2.Services
{
    public class BaseHttpClient
    {
#if DEBUG
        HttpClientHandler handler = new HttpClientHandler();
        protected HttpClient _client;


        public string uri = RestUri.uri;
        protected AuthenticationHeaderValue authHeader { get; set; }

        public string Token { get; set; }

        private HttpClientHandler config()
        {
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                return true;
              /*  if (cert.Issuer.Equals("CN=10.0.2.2"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;*/
            };
            return handler;
        }
#else
    HttpClient _client = new HttpClient();
#endif
        public BaseHttpClient()
        {
#if DEBUG 
            _client = new HttpClient(new LoggingHandler(config()));
#else
            _client = new HttpClient();
#endif
            _client.Timeout = TimeSpan.FromSeconds(20);
        }

        protected async Task<T> makeRequest<T>(
            HttpMethod method,
            Uri uri,
            string contentType,
            HttpContent content) where T : class
        {
            HttpRequestMessage request = new HttpRequestMessage(method, uri);
            request.Headers.Add("Accept", contentType);
            request.Content = content;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.User.Token);


            HttpResponseMessage response;

            response = await _client.SendAsync(request);

            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Exception("Please Re-login");
            }

            var stringContent = await response.Content.ReadAsStringAsync();

            T json = JsonConvert.DeserializeObject<T>(stringContent);

            return json;

        }
    }

    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString());
            if (response.Content != null)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            return response;
        }

        
    }
}
