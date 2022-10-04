using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Data;
using TelegramBot.Services;
using TgBotLib.Exceptions;
using TgBotLib.Handlers;
using TgBotLib.Services;
using TgBotLib.Utils;

namespace TelegramBot.Handlers
{
    public class RedactionHandler<TEntity> : BaseSpecialHandler where TEntity : class, IEntity
    {
        private TEntity entity { get; set; }
        private PropertyInfo property { get; set; }
        private readonly long chatId;
        private readonly string propertyName;
        private string redactionMessage;
        private readonly int entityId;

        public RedactionHandler(long chatId, string propertyName, int entityId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
            this.propertyName = propertyName;
            this.entityId = entityId;
        }
        public override async Task ProcessMessage(Message redactionMessage)
        {
            this.redactionMessage = redactionMessage.Text;
            await base.ProcessMessage(redactionMessage);
        }
        protected override void RegistrateProcessing()
        {
            AddProcessing("Введите значение", async () => await SetEntityProperty(), CompleteRedacton);
        }
        private async Task SetEntityProperty()
        {
            BaseCRUDService<TEntity, int> baseCRUDService = new(AppSettings.LessonsRoot);
            entity = await baseCRUDService.Get(entityId);
            property = entity.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
            property.SetValue(entity, Convert.ChangeType(redactionMessage, property.PropertyType));
        }
        private async void CompleteRedacton()
        {
            try
            {
                ButtonsGenerator buttonsGenerator = new ButtonsGenerator();
                BaseCRUDService<TEntity, int> baseCRUDService = new(AppSettings.LessonsRoot);
                await baseCRUDService.Update(entityId, entity);
                LogService.LogInfo($"|REDACTION| {propertyName}: {property}");
                buttonsGenerator.SetInlineButtons(("На главную", "/start"));
                await bot.SendMessage("Изменения сохранены", buttonsGenerator.GetButtons());
            }
            catch (ErrorResponseException)
            {
                await bot.SendMessage(Texts.Oops);
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
