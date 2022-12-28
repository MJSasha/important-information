using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface INewsService : ICrudService<News, int>
    {
        [Get("/Unsent")]
        Task<List<News>> GetUnsent();
        [Get("/ByLessonId/{lessonId}")]
        Task<List<News>> GetByLessonId(int lessonId);
        [Post("/ByDates")]
        Task<List<News>> GetByDates([Body] StartEndTime startEndTime);
        [Post("/AnyBefore")]
        Task<bool> CheckAnyNewsBefore([Body] DateTimeWrap date);
    }
}
