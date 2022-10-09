using ImpInfCommon.Interfaces;

namespace ImpInfCommon.Data.Models
{
    public class Password : IEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
