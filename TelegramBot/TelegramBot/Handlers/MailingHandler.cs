using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramBot.Data.Models;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using Telegram.Bot.Types;

namespace TelegramBot.Handlers
{
    public class MailingHandler : BaseSpecialHandler
    {
        private readonly long chatId;
        private PhotoSize[] photo;
        private News news = new News();
        public MailingHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        [Obsolete]
        public override async Task ProcessMessage(Message sendingMessage)
        {
            if (sendingMessage.Text == null){news.Message += sendingMessage.Caption;}
            else{ news.Message = sendingMessage.Text;}
            this.photo = sendingMessage.Photo;
            await base.ProcessMessage(sendingMessage);
        }

        [Obsolete]
        protected override void RegistrateProcessing()
        {
            AddProcessing("Напишите сообщение которое хотите отправить",
            () =>
            {
                    var dtn = DateTime.Now;
                    while ((DateTime.Now - dtn).Seconds < 3)
                    {
                            MailingProcessing(photo);
                    };
                if (photo != null){ news.AddPicture(photo[3].FileId); }
            }
            );
            SendAll();
        }

        protected void MailingProcessing(PhotoSize[] photo, Action completeAction = null)
        {
            сancellationToken = new();
            currentTask = new Task(() =>
            {
                if (photo != null) { news.AddPicture(photo[3].FileId); }
                сancellationToken.Cancel();
            });

            currentTask.Wait(10);
            completeAction?.Invoke();
        }

        [Obsolete]
        private async void SendAll()
        {
            try
            {
                var newsService = new NewsService();
                var userService = new UsersService(); 
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
