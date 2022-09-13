using System.ComponentModel.DataAnnotations;

namespace ImpInfCommon.Data.Other
{
    public class AuthModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
