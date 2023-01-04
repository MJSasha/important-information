using System.Collections.Generic;
using System.Threading.Tasks;
using ImpInfCommon.Data.Models;

namespace ImpInfApi.Interfaces
{
    public interface IScheduleParser
    {
        Task<List<Day>> GetSchedule();
    }
}
