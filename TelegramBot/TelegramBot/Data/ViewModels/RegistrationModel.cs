using System.ComponentModel.DataAnnotations;

namespace TelegramBot.Data.ViewModels
{
    public class RegistrationModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
