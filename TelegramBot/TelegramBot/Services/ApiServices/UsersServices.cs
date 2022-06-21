using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TelegramBot.Data.CustomExceptions;
using TelegramBot.Data.Models;

namespace TelegramBot.Services.ApiServices
{
    public class UsersServices : BaseCRUDService<User, int>
    {
        public UsersServices() : base(AppSettings.UsersRoot)
        {
        }

        public async Task Create(List<User> item)
        {
            var json = JsonSerializer.Serialize(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root, data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
