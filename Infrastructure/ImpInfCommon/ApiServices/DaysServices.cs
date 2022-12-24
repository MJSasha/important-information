using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using ImpInfCommon.Interfaces;
using ImpInfCommon.Utils;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImpInfCommon.ApiServices
{
    public class DaysServices : BaseCRUDService<Day, int>, IDaysService
    {
        private readonly IDaysService daysService;
        private readonly IErrorsHandler errorsHandler;

        public DaysServices(HttpClient httpClient, IErrorsHandler errorsHandler) : base(httpClient, errorsHandler)
        {
            daysService = UtilsFunctions.GetRefitService<IDaysService>(httpClient); ;
            this.errorsHandler = errorsHandler;
        }

        public async Task<List<Day>> GetByDates(StartEndTime startEndTime)
        {
            List<Day> result = default;
            await errorsHandler.SaveExecute(async () => result = await daysService.GetByDates(startEndTime));
            return result;
        }

        public async Task<Day> GetByDate(DateTimeWrap date)
        {
            Day result = default;
            await errorsHandler.SaveExecute(async () => result = await daysService.GetByDate(date));
            return result;
        }

        public async Task<bool> AnyBefore(DateTimeWrap date)
        {
            bool result = default;
            await errorsHandler.SaveExecute(async () => result = await daysService.AnyBefore(date));
            return result;
        }

        public async Task<bool> AnyAfter(DateTimeWrap date)
        {
            bool result = default;
            await errorsHandler.SaveExecute(async () => result = await daysService.AnyAfter(date));
            return result;
        }
    }
}
