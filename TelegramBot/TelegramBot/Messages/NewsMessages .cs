using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramBot.Data.CustomExceptions;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;

namespace TelegramBot.Messages
{
    public class NewsMessages
    {
        public static async void StartMailing()
        {
            while (true)
            {
                await SendNews();
                await Task.Delay((int)TimeSpan.FromMinutes(1).TotalMilliseconds);
            }
        }

        [Obsolete]
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
            catch (ChatNotFoundException ex)
            {
                LogService.LogError($"Chat not found | ChatId: {ex.ChatId}");
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());
            }
        }
    }
}
