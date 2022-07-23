using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Interfaces
{
    public interface IBotService
    {
        Task SendMessage(string message);
        Task SendMessage(string message, IReplyMarkup buttons);
        Task EditMessage(string message, int messageId);
        Task EditMessage(string message, IReplyMarkup buttons, int messageId);
    }
}
