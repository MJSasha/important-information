using ImpInfCommon.Data.Models;
using System.Net.Http;

namespace ImpInfCommon.ApiServices
{
    public class LessonsService : BaseCRUDService<Lesson, int>
    {
        public LessonsService(string backRoot, HttpClient httpClient) : base(backRoot, httpClient) { }
    }
}
