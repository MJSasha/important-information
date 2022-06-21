using System.Collections.Generic;

namespace TelegramBot.Models
{

    public class PostNotes
    {
        public List<PostNote> Notes { get; set; }
    }

    public class PostNote
    {
        public string Description { get; set; }
        public PostDayIn Day { get; set; }
        public PostUserIn User { get; set; }
    }

    public class PostDayIn
    {
        public int Id { get; set; }
    }

    public class PostUserIn
    {
        public int Id { get; set; }
    }

}
