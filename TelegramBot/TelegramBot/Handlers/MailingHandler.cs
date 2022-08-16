using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramBot.Data.Models;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using Telegram.Bot.Args;

namespace TelegramBot.Handlers
{
    public class MailingHandler : BaseSpecialHandler
    {
        private readonly long chatId;
        private string sendingMessage;
        private Telegram.Bot.Types.PhotoSize[] photo;
        public MailingHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        [Obsolete]
        public override async Task ProcessMessage(Telegram.Bot.Types.Message sendingMessage)
        {
            this.sendingMessage = sendingMessage.Text;
            this.photo = sendingMessage.Photo;
            await base.ProcessMessage(sendingMessage);
        }

        [Obsolete]
        protected override void RegistrateProcessing()
        {
            AddProcessing("Напишите сообщение которое хотите отправить", SendAll);
        }

        [Obsolete]
        private async void SendAll()
        {
            try
            {
                var newsService = new NewsService();
                var news = new News();
                var userService = new UsersService(); 
                news.Message = sendingMessage; 
                if (photo != null) { news.Pictures = photo.ToString(); }
                news.NeedToSend = false;
                await newsService.Create(news);
                var users = await userService.Get();
                if (news.Message != null)
                {
                    foreach (var user in users) { await BotService.SendMessage(news.Message, users.Select(u => u.ChatId).ToList()); }
                }
                if (news.Pictures != null) {
                    foreach (var user in users) { await BotService.SendPhoto(news.Pictures, users.Select(u => u.ChatId).ToList()); } 
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
