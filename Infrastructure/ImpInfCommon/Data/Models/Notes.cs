using ImpInfCommon.Interfaces;
using ImpInfCommon.Utils.Attributes;

namespace ImpInfCommon.Data.Models
{
    [EntityRoot("Notes")]
    public class Note : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Day Day { get; set; }
        public User User { get; set; }
    }
}
