using ImpInfCommon.Data.Definitions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImpInfCommon.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public Password Password { get; set; }
        public long ChatId { get; set; }
        public Role Role { get; set; }
        public List<Note> Notes { get; set; }
    }
}
