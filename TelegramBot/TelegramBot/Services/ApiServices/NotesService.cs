using TelegramBot.Data.Entities;
using TgBotLib.Services;

namespace TelegramBot.Services.ApiServices
{
    public class NotesService : BaseCRUDService<Note, int>
    {
        public NotesService() : base(AppSettings.NotesRoot)
        {
        }
    }
}
