using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TelegramBot.Data.CustomExceptions;


namespace TelegramBot.Services.ApiServices
{
    public class BaseCRUDService<TEntity, TKey> : BaseService
    {
        public BaseCRUDService(string entityRoot) : base(entityRoot) { }

        public async Task<TEntity> Get(TKey key)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root + "/" + key);
            return await Deserialize<TEntity>(httpResponse);
        }

        public async Task<List<TEntity>> Get()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root);
            return await Deserialize<List<TEntity>>(httpResponse);
        }

        public async Task Create(TEntity item)
        {
            var json = JsonSerializer.Serialize(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root, data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task Create(List<TEntity> item)
        {
            var json = JsonSerializer.Serialize(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root.ToString() + "/createAll", data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task Update(TKey key, TEntity item)
        {
            TEntity entity = await Get(key);
            if (entity == null) throw new ErrorResponseException(HttpStatusCode.NotFound);

            var json = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PatchAsync(Root + "/" + key, json);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task Delete(TKey key)
        {
            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(Root + "/" + key);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task Delete(List<TKey> key)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(key), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = Root
            };

            var response = await httpClient.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode) throw new ErrorResponseException(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        protected async Task<T> Deserialize<T>(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                var jsonRequest = await httpResponse.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonRequest);
            }
            throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
