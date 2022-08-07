using System;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using TelegramBot.Data.Models;
using TelegramBot.Messages;

namespace TelegramBot.Handlers
{
    public class SendAllHandler : BaseSpecialHandler
    {
        private readonly long chatId;
        private string sendingMessage;

        public SendAllHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        [Obsolete]
        public override async Task ProcessMessage(string sendingMessage)
        {
            this.sendingMessage = sendingMessage;

            if (сancellationToken == null) await Task.Run(() => SendAll());
            if (!сancellationToken.IsCancellationRequested) currentTask.Start();
        }

        [Obsolete]
        private void SendAll()
        {
            AddProcessing("Напишите сообщение которое хотите отправить", CompleteSend);
        }
        private async void CompleteSend()
        {
            try
            {
                var newsService = new NewsService();
                var news = new News();
                news.Message = sendingMessage;
                news.NeedToSend = true;
                await newsService.Create(news);
                NewsMessages.StartMailing();
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