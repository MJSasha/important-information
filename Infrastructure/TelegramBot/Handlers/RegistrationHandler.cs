using ImpInfCommon.Data.Other;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Data;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using TgBotLib.Exceptions;
using TgBotLib.Handlers;
using TgBotLib.Services;

namespace TelegramBot.Handlers
{
    public class RegistrationHandler : BaseSpecialHandler
    {
        private readonly RegistrationModel registrationModel = new();
        private readonly long chatId;
        private string registrationMassage;

        public RegistrationHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        public override async Task ProcessMessage(Message registrationMassage)
        {
            this.registrationMassage = registrationMassage.Text;
            await base.ProcessMessage(registrationMassage);
        }

        protected override void RegistrateProcessing()
        {
            AddProcessing("Введите ваше имя и фамилию", () => registrationModel.Name = registrationMassage);
            AddProcessing("Придумайте логин", () => registrationModel.Login = registrationMassage);
            AddProcessing("Придумайте пароль", () => registrationModel.Password = registrationMassage, CompleteRegistration);
        }

        private async void CompleteRegistration()
        {
            try
            {
                AuthService authService = new();
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
