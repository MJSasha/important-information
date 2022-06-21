using System;
using System.Net;
using System.Net.Http;

namespace TelegramBot.Services.ApiServices
{
    public class BaseService
    {
        protected readonly HttpClient httpClient;
        protected Uri Root { get; set; }

        public BaseService(string entityRoot)
        {
            Root = new Uri(AppSettings.BaseRoot + entityRoot);

            HttpClientHandler handler = new();
            handler.CookieContainer = new CookieContainer();
            handler.CookieContainer.Add(Root, new Cookie("token", AppSettings.TokenApi));
            httpClient = new HttpClient(handler);
        }
    }
}
