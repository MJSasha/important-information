using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TelegramBot.Data.CustomExceptions;
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
