using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramBot.Data.Models;

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

    }
}