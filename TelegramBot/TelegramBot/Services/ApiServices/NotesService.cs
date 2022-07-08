using TelegramBot.Data.Models;


namespace TelegramBot.Services.ApiServices
{
    public class NotesService : BaseCRUDService<Note, int>
    {
        public NotesService(string entityRoot) : base(AppSettings.NotesRoot)
        {
        }
    }
}
