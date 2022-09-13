namespace ImpInfCommon.Data.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Day Day { get; set; }
        public User User { get; set; }
    }
}
