using System;
using System.Linq;
using System.Net.Http;
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
            await Task.Run(() => { Timer timer = new(async (_) => await SendNews(), 0, 0, (int)TimeSpan.FromMinutes(1).TotalMilliseconds); });
        }

        private static async Task SendNews()
        {
            try
            {
                var newsService = new NewsService();
                var unsentNews = await newsService.GetUnsent();

                if (unsentNews != null)
                {
                    var userService = new UsersService();
                    var users = await userService.Get();

                    foreach (var news in unsentNews)
                    {
                        await BotService.SendMessage(news.Message, users.Select(u => u.ChatId).ToList());
                        news.NeedToSend = false;
                        await newsService.Update(news.Id, news);
                    }
                    LogService.LogInfo($"Sent {unsentNews.Count} news to {users.Count} users");
                }
            }
            catch (HttpRequestException)
            {
                LogService.LogServerNotFound("News mailing");
            }
        }
    }
}
