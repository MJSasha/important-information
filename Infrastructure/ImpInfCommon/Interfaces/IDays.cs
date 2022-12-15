using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IDays : ICrud<Day, int>
    {
        [Post("/ByDates")]
        Task<List<Day>> GetByDates([Body] StartEndTime startEndTime);
        [Post("/ByDate")]
        Task<Day> GetByDate([Body] DateTimeWrap date);
        [Post("/AnyBefore")]
        Task<bool> AnyBefore([Body] DateTimeWrap date);
        [Post("/AnyAfter")]
        Task<bool> AnyAfter([Body] DateTimeWrap date);
    }
}
