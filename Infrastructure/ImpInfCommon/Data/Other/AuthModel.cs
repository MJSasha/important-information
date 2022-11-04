using System.ComponentModel.DataAnnotations;

namespace ImpInfCommon.Data.Other
{
    public class AuthModel
    {
        [Required(ErrorMessage = "Поле логина должно быть заполнено")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Поле пароля должно быть заполнено")]
        public string Password { get; set; }
    }
}
