using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using ImpInfCommon.Interfaces;
using ImpInfCommon.Utils;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImpInfCommon.ApiServices
{
    public class NewsService : BaseCRUDService<News, int>, INewsService
    {
        private readonly INewsService newsService;
        private readonly IErrorsHandler errorsHandler;

        public NewsService(HttpClient httpClient, IErrorsHandler errorsHandler) : base(httpClient, errorsHandler)
        {
            newsService = UtilsFunctions.GetRefitService<INewsService>(httpClient); ;
            this.errorsHandler = errorsHandler;
        }

        public async Task<List<News>> GetUnsent()
        {
            List<News> result = default;
            await errorsHandler.SaveExecute(async () => result = await newsService.GetUnsent());
            return result;
        }

        public async Task<List<News>> GetByLessonId(int lessonId)
        {
            List<News> result = default;
            await errorsHandler.SaveExecute(async () => result = await newsService.GetByLessonId(lessonId));
            return result;
        }

        public async Task<List<News>> GetByDates(StartEndTime startEndTime)
        {
            List<News> result = default;
            await errorsHandler.SaveExecute(async () => result = await newsService.GetByDates(startEndTime));
            return result;
        }

        public async Task<bool> CheckAnyNewsBefore(DateTimeWrap date)
        {
            bool result = default;
            await errorsHandler.SaveExecute(async () => result = await newsService.CheckAnyNewsBefore(date));
            return result;
        }
    }
}