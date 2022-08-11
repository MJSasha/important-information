using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Data.Models;
using TelegramBot.Data.ViewModels;

namespace TelegramBot.Services.ApiServices
{
    public class NewsService : BaseCRUDService<News, int>
    {
        public NewsService() : base(AppSettings.NewsRoot)
        {

        }

        public async Task<List<News>> GetUnsent()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root + "/unsent");
            return await Deserialize<List<News>>(httpResponse);
        }

        public async Task<List<News>> Get(StartEndTime startEndTime)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(startEndTime), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Get,
                RequestUri = new Uri(Root.ToString() + "/byDates")
            };
            var httpResponse = await base.httpClient.SendAsync(httpRequest);
            return await Deserialize<List<News>>(httpResponse);
        }
    }
}