using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Data.Entities;
using TelegramBot.Data.SubModels;
using TgBotLib.Services;

namespace TelegramBot.Services.ApiServices
{
    public class DaysServices : BaseCRUDService<Day, int>
    {
        public DaysServices() : base(AppSettings.DaysRoot) { }

        public async Task<List<Day>> Get(StartEndTime startEndTime)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(startEndTime), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Get,
                RequestUri = new Uri(Root.ToString() + "/byDates")
            };
            var httpResponse = await base.httpClient.SendAsync(httpRequest);
            return await Deserialize<List<Day>>(httpResponse);
        }
    }
}
