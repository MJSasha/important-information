using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramBot.Data.Models;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using Telegram.Bot.Types;
using System.Threading;

namespace TelegramBot.Handlers
{
    public class MailingHandler : BaseSpecialHandler
    {
        private readonly long chatId;
        private string sendingMessage;
        private PhotoSize[] photo;
        private News news = new News();
        public MailingHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        [Obsolete]
        public override async Task ProcessMessage(Message sendingMessage)
        {
            if (sendingMessage.Text == null){this.sendingMessage = sendingMessage.Caption;}
            else{this.sendingMessage = sendingMessage.Text;}
            this.photo = sendingMessage.Photo;
            await base.ProcessMessage(sendingMessage);
        }

        [Obsolete]
        protected override void RegistrateProcessing()
        {
            AddProcessing("Напишите сообщение которое хотите отправить",
            () =>
            {
                if (photo != null)
                {
                    news.AddPicture(photo[3].FileId);
                    var dtn = DateTime.Now;
                    while ((DateTime.Now - dtn).Seconds < 2)
                    {
                        MailingProcessing(photo[3].FileId);
                    };
                }
            }
            );
            SendAll();
        }

        protected void MailingProcessing(string message, Action completeAction = null)
        {
            сancellationToken = new();
            currentTask = new Task(() =>
            {
                news.AddPicture(message);
                сancellationToken.Cancel();
            });
            news.AddPicture(message);
            currentTask.Wait(1000);
            completeAction?.Invoke();
        }

        [Obsolete]
        private async void SendAll()
        {
            try
            {
                var newsService = new NewsService();
                var userService = new UsersService(); 
                news.Message = sendingMessage;
                news.NeedToSend = false;
                await newsService.Create(news);
                var users = await userService.Get();

                if (news.Message != null)
                {
                    await BotService.SendMessage(news.Message, users.Select(u => u.ChatId).ToList()); 
                }
                if (news.Pictures != null) {
                    await BotService.SendPhoto(news.Pictures, users.Select(u => u.ChatId).ToList());
                }
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
