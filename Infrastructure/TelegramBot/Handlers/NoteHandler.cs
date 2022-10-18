using ImpInfCommon.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Data;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using TgBotLib.Handlers;
using TgBotLib.Services;
using TgBotLib.Utils;

namespace TelegramBot.Handlers
{
    public class NoteHandler : BaseSpecialHandler
    {
        private readonly long chatId;
        private readonly int dayId;
        private string redactionMessage;
        private Day chosenDay;

        public NoteHandler(long chatId, Day chosenDay, int dayId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
            this.chosenDay = chosenDay;
            this.dayId = dayId;
        }

        public override async Task ProcessMessage(Message redactionMessage)
        {
            this.redactionMessage = redactionMessage.Text;
            await base.ProcessMessage(redactionMessage);
        }

        protected override void RegistrateProcessing()
        {
            AddProcessing("Введите значение", AddNote);
        }

        private async void AddNote()
        {
            try
            {
                UsersService usersService = new();
                var user = await usersService.GetByChatId(chatId);

                Note note = new()
                {
                    Description = redactionMessage,
                    User = user,
                    Day = chosenDay,
                };

                NotesService notesService = new();
                await notesService.Create(note);

                ButtonsGenerator buttonsGenerator = new();
                buttonsGenerator.SetInlineButtons(("На главную", "/start"));
                await bot.SendMessage("Изменения сохранены", buttonsGenerator.GetButtons());
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());
                await bot.SendMessage(Texts.Oops);
            }
            finally
            {
                DistributionService.BusyUsersIdAndService.Remove(chatId);
            }
        }
    }
}
