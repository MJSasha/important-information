using ImpInfCommon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using TgBotLib.Handlers;

namespace TelegramBot.Handlers
{
    public class NoteHandler: BaseSpecialHandler
    {
        private readonly long chatId;
        private string redactionMessage;
        private Day chosenDay;
        public NoteHandler(long chatId, Day chosenDay) : base(new BotService(chatId))
        {
            this.chatId = chatId;
            this.chosenDay = chosenDay;
        }

        public override async Task ProcessMessage(Message redactionMessage)
        {
            this.redactionMessage = redactionMessage.Text;
            await base.ProcessMessage(redactionMessage);
        }

        protected override void RegistrateProcessing()
        {
            AddProcessing("Введите значение", null);
        }
    }
}
