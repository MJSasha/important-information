using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TgBotLib.Exceptions;
using TgBotLib.Utils;

namespace ImpInfCommon.ApiServices
{
    public class BaseCRUDService<TEntity, TKey> : BaseService where TEntity : class
    {
        public BaseCRUDService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(entityRoot ?? typeof(TEntity).GetRoot(), backRoot, httpClient) { }

        public virtual async Task<TEntity> Get(TKey key)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root + "/" + key);
            return await Deserialize<TEntity>(httpResponse);
        }

        public virtual async Task<List<TEntity>> Get()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root);
            return await Deserialize<List<TEntity>>(httpResponse);
        }

        public virtual async Task Create(TEntity item)
        {
            var json = Serialize(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root, data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public virtual async Task Create(List<TEntity> item)
        {
            var json = Serialize(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root.ToString() + "/createAll", data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public virtual async Task Update(TKey key, TEntity item)
        {
            TEntity entity = await Get(key);
            if (entity == null) throw new ErrorResponseException(HttpStatusCode.NotFound, "Entity not found");

            var json = new StringContent(Serialize(item), Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PatchAsync(Root + "/" + key, json);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public virtual async Task Delete(TKey key)
        {
            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(Root + "/" + key);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public virtual async Task Delete(List<TKey> key)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Content = new StringContent(Serialize(key), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = Root
            };

            var response = await httpClient.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode) throw new ErrorResponseException(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}