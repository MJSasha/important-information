using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface INewsServices : IBaseCRUDService<News, int>
    {
        Task<List<News>> GetUnsent();
        Task<News[]> GetByLessonId(int lessonId);
        Task<List<News>> Get(StartEndTime startEndTime);
        Task<bool> CheckNewsBefore(DateTime date);
    }
}
