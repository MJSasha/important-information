using ImpInfCommon.Data.Definitions;
using ImpInfCommon.Interfaces;
using ImpInfCommon.Utils.Attributes;
using System.Collections.Generic;

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
        public string Token { get; set; }
        public long ChatId { get; set; }
        public Role Role { get; set; }
        public List<Note> Notes { get; set; }
    }
}
