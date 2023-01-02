using ImpInfCommon.Utils.Attributes;

namespace ImpInfCommon.Data.Definitions
{
    public enum Role
    {
        [EnumName("Пользователь")]
        USER,
        [EnumName("Администратор")]
        ADMIN
    }
}
