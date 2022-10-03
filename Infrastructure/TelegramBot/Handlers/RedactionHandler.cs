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
        private async Task SetEntityProperty(string redactionMessage)
        {
            BaseCRUDService<TEntity, int> baseCRUDService = new(AppSettings.LessonsRoot);
            TEntity entity = await baseCRUDService.Get(entityId);
            PropertyInfo property = entity.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
            property.SetValue(entity, Convert.ChangeType(redactionMessage, property.PropertyType));
            await baseCRUDService.Update(entityId, entity);
        }
        public override async Task ProcessMessage(Message redactionMessage)
        {
            this.redactionMessage = redactionMessage.Text;
            await base.ProcessMessage(redactionMessage);
        }
        protected override void RegistrateProcessing()
        {
            AddProcessing("Введите значение", async () => await SetEntityProperty(redactionMessage), CompleteRedacton);
        }
        private async void CompleteRedacton()
        {
            try
            {
                ButtonsGenerator buttonsGenerator = new ButtonsGenerator();
                buttonsGenerator.SetInlineButtons(("На главную", "/start"));
                await bot.SendMessage("Изменения сохранены", buttonsGenerator.GetButtons());
            }
            catch
            {
                await bot.SendMessage(Texts.Oops);
            }
            finally
            {
                DistributionService.BusyUsersIdAndService.Remove(chatId);
            }
        }
    }
}
