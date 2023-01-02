using ImpInfCommon.Interfaces;
using ImpInfCommon.Utils;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImpInfCommon.ApiServices
{
    public class BaseCRUDService<TEntity, TKey> : ICrudService<TEntity, TKey> where TEntity : class, IEntity
    {
        private readonly ICrudService<TEntity, TKey> crudService;
        private readonly IErrorsHandler errorsHandler;

        public BaseCRUDService(HttpClient httpClient, IErrorsHandler errorsHandler)
        {
            httpClient.BaseAddress = new System.Uri($"{httpClient.BaseAddress}/{typeof(TEntity).GetRoot()}");
            crudService = UtilsFunctions.GetRefitService<ICrudService<TEntity, TKey>>(httpClient);
            this.errorsHandler = errorsHandler;
        }

        public virtual async Task<TEntity> Get(TKey key)
        {
            TEntity result = default;
            await errorsHandler.SaveExecute(async () => result = await crudService.Get(key));
            return result;
        }

        public virtual async Task<List<TEntity>> Get()
        {
            List<TEntity> result = default;
            await errorsHandler.SaveExecute(async () => result = await crudService.Get());
            return result;
        }

        public virtual Task Post(TEntity item)
        {
            return errorsHandler.SaveExecute(async () => await crudService.Post(item));
        }

        public virtual Task Post(List<TEntity> item)
        {
            return errorsHandler.SaveExecute(async () => await crudService.Post(item));
        }

        public virtual Task Patch(TKey key, TEntity item)
        {
            return errorsHandler.SaveExecute(async () => await crudService.Patch(key, item));
        }

        public virtual Task Delete(TKey key)
        {
            return errorsHandler.SaveExecute(async () => await crudService.Delete(key));
        }
    }
}