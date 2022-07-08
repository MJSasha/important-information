using TelegramBot.Data.Models;


namespace TelegramBot.Services.ApiServices
{
    public class NotesService : BaseCRUDService<Note, int>
    {
        public NotesService() : base(AppSettings.NotesRoot)
        {
        }
    }
}
