using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Interfaces
{
    public interface IBotActions
    {
        Task SendMessage(string message);
        Task SendMessage(string message, IReplyMarkup buttons);
    }
}
