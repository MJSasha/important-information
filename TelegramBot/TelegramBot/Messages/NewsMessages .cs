using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;

namespace TelegramBot.Messages
{
    public class NewsMessages
    {
        public static async Task StartMailing()
        {
            await Task.Run(() => { Timer timer = new Timer(async (_) => await SendNews(), 0, 0, (int)TimeSpan.FromMinutes(5).TotalMilliseconds); });
        }

        private static async Task SendNews()
        {
            var newsService = new NewsService();
            var unsentNews = await newsService.GetUnsent();

            if (unsentNews != null)
            {
                var userService = new UsersServices();
                var users = await userService.Get();

                foreach (var news in unsentNews)
                {
                    await BotService.SendMessage(news.Message, users.Select(u => u.ChatId).ToList());
                    news.NeedToSend = false;
                    await newsService.Update(news.Id, news);
                }
            }
        }
    }
}
