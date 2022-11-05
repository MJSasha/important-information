using ImpInfCommon.ApiServices;
using ImpInfCommon.Data.Other;
using ImpInfCommon.Exceptions;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Data;
using TelegramBot.Services;
using TgBotLib.Exceptions;
using TgBotLib.Handlers;
using TgBotLib.Services;
using TgBotLib.Utils;

namespace TelegramBot.Handlers
{
    public class RegistrationHandler : BaseSpecialHandler
    {
        private readonly RegistrationModel registrationModel = new();
        private readonly long chatId;
        private string registrationMessage;

        public RegistrationHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        public override async Task ProcessMessage(Message registrationMessage)
        {
            this.registrationMessage = registrationMessage.Type == MessageType.Contact ?
                registrationMessage.Contact.PhoneNumber : registrationMessage.Text;
            await base.ProcessMessage(registrationMessage);
        }

        protected override void RegistrateProcessing()
        {
            AddProcessing("Введите ваше имя и фамилию", () => registrationModel.Name = registrationMessage);
            AddProcessing("Введите ваш телефон", () => registrationModel.Phone = registrationMessage, button: ButtonsGenerator.GetKeyboardButtonWithPhoneRequest("Отправить телефон"));
            AddProcessing("Придумайте логин", () => registrationModel.Login = registrationMessage, button: new ReplyKeyboardRemove());
            AddProcessing("Придумайте пароль", () => registrationModel.Password = registrationMessage, CompleteRegistration);
        }

        private async void CompleteRegistration()
        {
            try
            {
                AuthService authService = TransientService.GetAuthService();
                await authService.Registrate(registrationModel, chatId);
                LogService.LogInfo($"|REGISTRATION| ChatId: {chatId} | Name: {registrationModel.Name} | Login: {registrationModel.Login}");
                await bot.SendMessage($"Вы зарегистрированны! Теперь я буду обращаться к вам по имени {registrationModel.Name}");
            }
            catch (ErrorResponseException)
            {
                await bot.SendMessage("Мде... походу такой логин уже существует, дружище. Давай по новой (/reg)");
            }
            catch (HttpRequestException)
            {
                LogService.LogServerNotFound("Registration");
                await bot.SendMessage(Texts.Oops);
            }
            finally
            {
                DistributionService.BusyUsersIdAndService.Remove(chatId);
            }
        }
    }
}
