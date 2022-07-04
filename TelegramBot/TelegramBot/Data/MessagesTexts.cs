using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Services;
using TelegramBot.Interfaces;

namespace TelegramBot.Data
{
    public class MessagesTexts
    {
        private readonly IBotService bot;

        public string AboutUs = "мы что-то сделали";

        [Obsolete]
        public MessagesTexts(long chatId)
        {
            bot = new BotService(chatId);
        }
        public Func<Task> Information()
        {
            return () => bot.SendMessage(AboutUs);
        }
    }
}
