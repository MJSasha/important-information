using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IDaysService : ICrudService<Day, int>
    {
        Task<List<Day>> GetByDates(StartEndTime startEndTime);
        Task<Day> GetByDates(DateTimeWrap date);
        Task<bool> AnyBefore(DateTimeWrap date);
        Task<bool> AnyAfter(DateTimeWrap date);
    }
}
