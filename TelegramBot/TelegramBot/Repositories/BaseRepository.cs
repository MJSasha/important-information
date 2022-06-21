using Newtonsoft.Json;
using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks; //TemporaryFunctions

namespace TelegramBot.Repositories
{
    public class BaseRepository<TEntity, TKey>
    {
        public Uri Root { get; set; }
        public string Token { get; set; }
        private readonly HttpClient httpClient = new();

        public BaseRepository(string entityRoot)
        {
            Root = new Uri(AppSettings.BaseRoot + entityRoot);
            Token = AppSettings.TokenApi;

            HttpClientHandler handler = new();
            handler.CookieContainer = new CookieContainer();
            handler.CookieContainer.Add(this.Root, new Cookie("token", Token));
            httpClient = new HttpClient(handler);
        }

        public async Task<TEntity> Get(TKey key)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root + "/" + key);
            if (httpResponse.IsSuccessStatusCode)
            {
                return await Deserialize<TEntity>(httpResponse);
            }
            throw new Exception("Get(TKey key): - StatusCode - "
                                + httpResponse.StatusCode
                                + " Content - "
                                + await httpResponse.Content.ReadAsStringAsync());

        }

        public async Task<TEntity> GetCurrentUser()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root);
            if (httpResponse.IsSuccessStatusCode)
            {
                return await Deserialize<TEntity>(httpResponse);
            }
            throw new Exception("Get(TKey key): - StatusCode - "
                                + httpResponse.StatusCode
                                + " Content - "
                                + await httpResponse.Content.ReadAsStringAsync());
        }
    
        public async Task<List<TEntity>> Get()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root);
            if (httpResponse.IsSuccessStatusCode)
            {
                return await Deserialize<List<TEntity>>(httpResponse);
            }
            throw new Exception("Get(): - StatusCode - "
                                + httpResponse.StatusCode
                                + " Content - "
                                + await httpResponse.Content.ReadAsStringAsync()); 
        }
        public async Task<HttpResponseMessage> Delete(TKey key) 
        {
            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(Root + "/" + key);
            return httpResponse;
        }

        //public async Task Delete(TKey key) 
        //{
        //    HttpResponseMessage httpResponse = await httpClient.DeleteAsync(Root + "/" + key);
        //    if (httpResponse.IsSuccessStatusCode)
        //    {
        //        Console.WriteLine(await httpResponse.Content.ReadAsStringAsync());
        //    }
        //    throw new Exception("Delete(): - StatusCode - "
        //                       + httpResponse.StatusCode
        //                       + " Content - "
        //                       + await httpResponse.Content.ReadAsStringAsync());
        //}

        public async Task<HttpResponseMessage> Delete(List<TKey> key)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(key), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = Root
            };
            
            return await httpClient.SendAsync(httpRequest);
        } 

        public async Task<HttpResponseMessage> Update(TKey key, TEntity item) // TEntity?
        {
            TEntity entity = await Get(key);
            if(entity == null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
            var json = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await httpClient.PatchAsync(Root + "/" + key, json); 
            return httpResponse;
        }


        public async Task<HttpResponseMessage> Post(TEntity item)
        {
            var json = JsonConvert.SerializeObject(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root, data);
            return httpResponse;
        }
        
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                var jsonRequest = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonRequest);
            }
            throw new Exception("Deserialize: Печаль");
        }
    }
}
