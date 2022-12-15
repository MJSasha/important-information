using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface INewsService : ICrudService<News, int>
    {
        Task<List<News>> GetUnsent();
        Task<List<News>> GetByLessonId(int lessonId);
        Task<List<News>> GetByDates(StartEndTime startEndTime);
        Task<bool> CheckAnyNewsBefore(DateTimeWrap date);
    }
}
