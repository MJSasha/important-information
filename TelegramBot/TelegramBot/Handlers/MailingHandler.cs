using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Data.Models;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;

namespace TelegramBot.Handlers
{
    public class MailingHandler : BaseSpecialHandler
    {
        private readonly long chatId;
        private string sendingText;
        private string caption;
        private PhotoSize[] photo;
        private readonly News news = new News();
        public MailingHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        [Obsolete]
        public override async Task ProcessMessage(Message sendingMessage)
        {
            if (sendingMessage.Caption == null) sendingText = sendingMessage.Text;
            else caption = sendingMessage.Caption;
            photo = sendingMessage.Photo;
            await base.ProcessMessage(sendingMessage);
        }

        [Obsolete]
        protected override void RegistrateProcessing()
        {
            AddProcessing("Напишите сообщение которое хотите отправить", int.MaxValue, () =>
            {
                news.Message += sendingText;
                var dtn = DateTime.Now;
                while ((DateTime.Now - dtn).Seconds < 3)
                {
                        MailingProcessing(photo);
                    if (photo != null) { news.AddPicture(photo[^1].FileId); }
                };
            });
            SendAll();
        }
        protected void MailingProcessing(PhotoSize[] photo, Action completeAction = null)
        {
            сancellationToken = new();
            currentTask = new Task(() =>
            {
                if (photo != null) news.AddPicture(photo[^1].FileId);
                сancellationToken.Cancel();
            });
            currentTask.Wait(10);
        }

        [Obsolete]
        private async void SendAll()
        {
            try
            {
                var newsService = new NewsService();
                var userService = new UsersService();
                news.NeedToSend = false;
                if (caption != null) news.Message = caption;
                await newsService.Create(news);
                var users = await userService.Get();

                await BotService.SendPhoto(news, users.Select(u => u.ChatId).ToList());
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
