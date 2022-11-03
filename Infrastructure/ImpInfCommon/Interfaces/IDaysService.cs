using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IDays
    {
        Task<Day[]> GetByDates(StartEndTime startEndTime);
        Task<Day> GetByDates(DateTimeWrap date);
        Task<bool> AnyBefore(DateTimeWrap date);
        Task<bool> AnyAfter(DateTimeWrap date);
    }
}
