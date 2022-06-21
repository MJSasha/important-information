using System.Collections.Generic;
using TelegramBot.Models;

namespace TelegramBot.Repositories
{
    public class StartModelsRepository : BaseRepository<List<User>, int>
    {
        public StartModelsRepository() : base(AppSettings.StartModels)
        {
        }
    }
}
