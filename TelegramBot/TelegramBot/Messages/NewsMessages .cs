using System;
using System.Linq;
using System.Threading;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using TelegramBot.Interfaces;

namespace TelegramBot.Messages
{
    public class NewsMessages
    {
        private readonly IBotService bot;
        public void StartMailing()
        {
            int num = 0;
            TimerCallback tm = new TimerCallback(UnsentNews);
            Timer timer = new Timer(tm, num, 0, 30000);
        }
        private async void UnsentNews(object obj)
        {
            var newsService = new NewsService();
            var unsentNews = await newsService.GetUnsent();
            if (unsentNews != null)
            {
                var userService = new UsersServices();
                var users = await userService.Get();
                foreach (long chatId in users.Select(u => u.ChatId).ToList())
                {
                    foreach (var message in unsentNews.Select(m => m.Message).ToList())
                    {

                        
                        await bot.SendMessage(message, chatId);

                    }
                }
                unsentNews.ForEach(newsinfo => newsinfo.NeedToSend = false);
            }
        }
    }
}
