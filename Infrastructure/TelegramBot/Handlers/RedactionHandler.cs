using ImpInfCommon.Interfaces;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Data;
using TelegramBot.Services;
using TgBotLib.Handlers;
using TgBotLib.Services;
using TgBotLib.Utils;

namespace TelegramBot.Handlers
{
    public class RedactionHandler<TEntity> : BaseSpecialHandler where TEntity : class, IEntity
    {
        private readonly int entityId;
        private readonly long chatId;
        private readonly string propertyName;
        private string redactionMessage;

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
            AddProcessing("Введите значение", async () => await RedactEntity());
        }

        private async Task RedactEntity()
        {
            try
            {
                BaseCRUDService<TEntity, int> baseCRUDService = new();
                var entity = await baseCRUDService.Get(entityId);
                var property = entity.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
                property.SetValue(entity, Convert.ChangeType(redactionMessage, property.PropertyType));

                await baseCRUDService.Update(entityId, entity);

                LogService.LogInfo($"|REDACTION| {typeof(TEntity).Name}.{propertyName}: {redactionMessage}");

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
