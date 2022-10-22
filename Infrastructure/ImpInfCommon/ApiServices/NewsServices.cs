using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImpInfCommon.ApiServices
{
    public class NewsService : BaseCRUDService<News, int>
    {
        public NewsService(string backRoot, HttpClient httpClient) : base(backRoot, httpClient) { }

        public async Task<List<News>> GetUnsent()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root + "/Unsent");
            return await Deserialize<List<News>>(httpResponse);
        }

        public async Task<News[]> GetByLessonId(int lessonId)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/ByLessonId/{lessonId}");
            return await Deserialize<News[]>(httpResponse);
        }

        public async Task<List<News>> Get(StartEndTime startEndTime)
        {
            var httpResponse = await httpClient.PostAsync(Root.ToString() + "/ByDates", new StringContent(Serialize(startEndTime), Encoding.UTF8, "application/json"));
            return await Deserialize<List<News>>(httpResponse);
        }

        public async Task<bool> CheckNewsBefore(DateTime date)
        {
            var httpResponse = await httpClient.PostAsync(Root.ToString() + "/AnyBefore", new StringContent(Serialize(new DateTimeWrap { DateTime = date }), Encoding.UTF8, "application/json"));
            return await Deserialize<bool>(httpResponse);
        }
    }
}