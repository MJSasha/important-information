using ImpInfCommon.Interfaces;

namespace ImpInfCommon.Data.Models
{
    public class Note : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Day Day { get; set; }
        public User User { get; set; }
    }
}
