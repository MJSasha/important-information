
namespace TelegramBot.Models
{
    public class PostUser
    {
        public string name { get; set; }
        public string login { get; set; }
        public long? ChatId { get; set; }
        public Password password { get; set; }
        public Role role { get; set; }

        public PostUser()
        {
            name = "mlexanders123";
            login = "GoGo.co123m";
            password = new Password();
            role = Role.ADMIN;
        }  
    }
}
