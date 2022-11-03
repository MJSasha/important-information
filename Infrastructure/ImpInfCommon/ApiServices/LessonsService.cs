using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using System.Net.Http;

namespace ImpInfCommon.ApiServices
{
    public class LessonsService : BaseCRUDService<Lesson, int>, ILesson
    {
        public LessonsService(string backRoot, HttpClient httpClient) : base(backRoot, httpClient) { }
    }
}
