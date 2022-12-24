using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface ICrudService<TEntity, TKey> where TEntity : class, IEntity
    {
        [Get("")]
        Task<List<TEntity>> Get();
        [Get("/{id}")]
        Task<TEntity> Get(TKey id);
        [Post("")]
        Task Post([Body] TEntity item);
        [Post("")]
        Task Post([Body] List<TEntity> item);
        [Patch("/{id}")]
        Task Patch([AliasAs("id")] TKey key, TEntity item);
        [Delete("/{id}")]
        Task Delete([AliasAs("id")] TKey key);
    }
}
