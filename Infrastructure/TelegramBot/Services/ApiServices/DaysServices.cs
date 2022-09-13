using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TgBotLib.Services;

namespace TelegramBot.Services.ApiServices
{
    public class DaysServices : BaseCRUDService<Day, int>
    {
        public DaysServices() : base(AppSettings.DaysRoot) { }

        public async Task<List<Day>> Get(StartEndTime startEndTime)
        {
            var data = new StringContent(Serialize(startEndTime), Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root.ToString() + "/ByDates", data);
            return await Deserialize<List<Day>>(httpResponse);
        }
    }
}
