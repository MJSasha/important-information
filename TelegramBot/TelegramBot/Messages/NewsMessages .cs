using System;
using System.Linq;
using System.Threading;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;

namespace TelegramBot.Messages
{
    public class NewsMessages
    {
        public NewsMessages()
        {
        }

        public void StartMailing()
        {
            int num = 0;
            TimerCallback tm = new TimerCallback(ChekNews);
            Timer timer = new Timer(tm, num, 0, 3000);
        }
        private async void ChekNews(object obj)
        {
            var newsService = new NewsService();
            var newsinfo = await newsService.GetUnsent();
            if (newsinfo != null)
            {
                var userService = new UsersServices();
                var users = await userService.Get();
                foreach (long chatId in users.Select(u => u.ChatId).ToList())
                {
                    foreach (var message in newsinfo.Select(m => m.Message).ToList())
                    {

                        await new BotService(chatId).SendMessage(message, chatId);

                    }
                }
                newsinfo.ForEach(newsinfo => newsinfo.NeedToSend = false);
            }
        }
    }
}
