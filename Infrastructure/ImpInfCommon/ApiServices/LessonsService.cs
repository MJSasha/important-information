using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;

namespace ImpInfCommon.ApiServices
{
    public class LessonsService : BaseCRUDService<Lesson, int>
    {
        public LessonsService(string backRoot, ITokenProvider tokenProvider) : base(backRoot, tokenProvider) { }
    }
}
