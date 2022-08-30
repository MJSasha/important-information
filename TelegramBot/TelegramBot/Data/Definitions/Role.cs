using TgBotLib.Utils.Attributes;

namespace TelegramBot.Data.Definitions
{
    public enum Role
    {
        [EnumName("Пользователь")]
        USER,
        [EnumName("Администратор")]
        ADMIN
    }
}
