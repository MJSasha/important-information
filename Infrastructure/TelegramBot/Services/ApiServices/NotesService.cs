using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TgBotLib.Services;

namespace TelegramBot.Services.ApiServices
{
    public class NotesService : BaseCRUDService<Note, int>
    {
        public NotesService() { }
        public async Task<Note> Get(int noteId)
        {
            var data = new StringContent(Serialize(noteId), Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root.ToString() + "/ByDate", data);
            return await Deserialize<Note>(httpResponse);
        }
    }
}
