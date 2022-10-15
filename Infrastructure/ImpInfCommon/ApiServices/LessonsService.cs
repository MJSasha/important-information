using ImpInfCommon.Data.Models;

namespace ImpInfCommon.ApiServices
{
    public class LessonsService : BaseCRUDService<Lesson, int>
    {
        public LessonsService(string backRoot, string token = "") : base(backRoot, token: token) { }
    }
}
