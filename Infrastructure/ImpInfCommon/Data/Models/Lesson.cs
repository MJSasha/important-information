using ImpInfCommon.Interfaces;
using ImpInfCommon.Utils.Attributes;

namespace ImpInfCommon.Data.Models
{
    [EntityRoot("Lessons")]
    public class Lesson : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Information { get; set; }
    }
}
