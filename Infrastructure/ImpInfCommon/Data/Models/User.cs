using ImpInfCommon.Data.Definitions;
using ImpInfCommon.Interfaces;
using System.Collections.Generic;
using TgBotLib.Utils.Attributes;

namespace ImpInfCommon.Data.Models
{
    [EntityRoot("Users")]
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public Password Password { get; set; }
        public long ChatId { get; set; }
        public Role Role { get; set; }
        public List<Note> Notes { get; set; }
    }
}
