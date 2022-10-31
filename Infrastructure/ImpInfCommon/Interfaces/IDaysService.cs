using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IDaysService : IBaseCRUDService<Day, int>
    {
        Task<List<Day>> Get(StartEndTime startEndTime);
        Task<Day> Get(DateTimeWrap date);
        Task<bool> AnyBefore(DateTimeWrap date);
        Task<bool> AnyAfter(DateTimeWrap date);
    }
}
