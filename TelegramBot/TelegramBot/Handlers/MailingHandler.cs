using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Data.Entities;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using TgBotLib.Exceptions;
using TgBotLib.Handlers;
using TgBotLib.Services;

namespace TelegramBot.Handlers
{
    public class MailingHandler : BaseSpecialHandler
    {
        private readonly long chatId;
        private string messageText;
        private bool messageIsStartSending;
        private readonly News news = new();

        public MailingHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        public override async Task ProcessMessage(Message sendingMessage)
        {
            messageText ??= sendingMessage.Caption ?? sendingMessage.Text;
            await base.ProcessMessage(sendingMessage);

            if (sendingMessage.Photo != null) news.AddPicture(sendingMessage.Photo[^1].FileId);
            else if (!messageIsStartSending)
            {
                messageIsStartSending = true;
                await SendAll();
            }
        }

        protected override void RegistrateProcessing()
        {
            AddProcessing("Напишите сообщение которое хотите отправить (ВНИМАНИЕ: не больше 10 картинок", null);
        }

        private async Task SendAll()
        {
            var newsService = new NewsService();
            var userService = new UsersService();

            try
            {
                news.Message = messageText;
                await newsService.Create(news);
                var chatIds = (await userService.Get()).Select(u => u.ChatId).Where(id => id != chatId).ToList();

                await BotService.SendNews(news, chatIds);
                LogService.LogInfo($"|SENDALL| ChatId: {chatId} | Message: {news.Message} | NeedToSend: {news.NeedToSend}");
                await bot.SendMessage($"Сообщение отправлено!");
            }
            catch (ChatNotFoundException ex)
            {
                LogService.LogError($"Chat not found | ChatId: {ex.ChatId}");
                await bot.SendMessage($"Не найден пользователь с ChatId {ex.ChatId}. Необходимо удалить данного пользователя для продолжения работы");
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
