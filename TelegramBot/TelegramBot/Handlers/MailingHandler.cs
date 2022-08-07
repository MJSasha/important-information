using System;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using TelegramBot.Data.Models;
using System.Linq;

namespace TelegramBot.Handlers
{
    public class MailingHandler : BaseSpecialHandler
    {
        private readonly long chatId;
        private string sendingMessage;

        public MailingHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        [Obsolete]
        public override async Task ProcessMessage(string sendingMessage)
        {
            this.sendingMessage = sendingMessage;

            if (сancellationToken == null) await Task.Run(() => Mailing());
            if (!сancellationToken.IsCancellationRequested) currentTask.Start();
        }

        [Obsolete]
        private void Mailing()
        {
            AddProcessing("Напишите сообщение которое хотите отправить", SendAll);
        }
        private async void SendAll()
        {
            try
            {
                var newsService = new NewsService();
                var news = new News();
                var userService = new UsersService();
                news.Message = sendingMessage;
                news.NeedToSend = false;
                await newsService.Create(news);
                var users = await userService.Get();
                foreach (var user in users) { await BotService.SendMessage(sendingMessage, users.Select(u => u.ChatId).ToList()); }
                LogService.LogInfo($"|SENDALL| ChatId: {chatId} | Message: {news.Message} | NeedToSend: {news.NeedToSend}");
                await bot.SendMessage($"Сообщение отправлено!");
            }
            catch (HttpRequestException)
            {
                LogService.LogServerNotFound("SendAll");
                await bot.SendMessage("Упс, что-то пошло не так...");
            }
            finally
            {
                DistributionService.BusyUsersIdAndService.Remove(chatId);
            }
        }
    }
}
