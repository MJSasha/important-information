using ImpInfCommon.Interfaces;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using TgBotLib.Handlers;
using TgBotLib.Services;

namespace TelegramBot.Handlers
{
    public class RedactionHandler<TEntity> : BaseSpecialHandler where TEntity : class, IEntity
    {
        private static TEntity entity { get; set; }
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
        public static async Task GetEntityField(string redactionMessage, string propertyName, int entityId)
        {
            BaseCRUDService<TEntity, int> baseCRUDService = new(AppSettings.LessonsRoot);
            entity = await baseCRUDService.Get(entityId);
            PropertyInfo property = entity.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
            property.SetValue(entity, Convert.ChangeType(redactionMessage, property.PropertyType));
        }
        public override async Task ProcessMessage(Message redactionMessage)
        {
            this.redactionMessage = redactionMessage.Text;
            await base.ProcessMessage(redactionMessage);
        }
        protected override async void RegistrateProcessing()
        {
            AddProcessing("Введите значение", async () => await GetEntityField(redactionMessage, propertyName, entityId));
        }
    }
}
