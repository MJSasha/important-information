using ImpInfCommon.Data.Models;

namespace ImpInfCommon.Interfaces
{
    public interface INews : IBaseCRUD<News>
    {
        //Task<List<News>> GetUnsent();
        //Task<News[]> GetByLessonId(int lessonId);
        //Task<List<News>> Get(StartEndTime startEndTime);
        //Task<bool> CheckNewsBefore(DateTime date);
    }
}
