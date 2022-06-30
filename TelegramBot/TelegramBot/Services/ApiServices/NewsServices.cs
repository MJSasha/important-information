using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Data.Models;
using TelegramBot.Data.ViewModels;

namespace TelegramBot.Services.ApiServices
{
    public class NewsService : BaseCRUDService<News, int>
    {
        public NewsService() : base(AppSettings.NotesRoot)   //конструктор пока не нужен
        {
        }

        //оба метода ниже возвращают ссылки и прогоняют в методе Deserialize
        public async Task<List<News>> Get(News news)        //  Get news by id  Берем новости с сервака   Вид ссылки: {{baseUrl}}/news/{id}
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(news), Encoding.UTF8, "application/json"),  //{baseUrl}
                Method = HttpMethod.Get,                                                                            //news
                RequestUri = new Uri(AppSettings.TokenApi)                                                               //id
            };
            var httpResponse = await base.httpClient.SendAsync(httpRequest);
            return await Deserialize<List<News>>(httpResponse);
        }   //обновлять каждые 5 минут

        public async Task<List<News>> Add(News news)        //  Add news    Добавлять новости на сервак Вид ссылки: {{baseUrl}}/news
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(news), Encoding.UTF8, "application/json"),  //{baseUrl}
                Method = HttpMethod.Get                                                                            //news
            };
            var httpResponse = await base.httpClient.SendAsync(httpRequest);
            //возможно не нужно
            News seter = new News();
            seter.Id = news.Id;
            seter.Message = news.Message;
            seter.NeedToSend = false;
            //
            return await Deserialize<List<News>>(httpResponse);

        }   //Запускать с заданной моделью news после задания самой модели
    }
}

