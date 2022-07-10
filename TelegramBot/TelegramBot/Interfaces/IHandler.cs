using System.Threading.Tasks;

namespace TelegramBot.Interfaces
{
    public interface IHandler
    {
        public Task ProcessMessage(string message);
    }
}
