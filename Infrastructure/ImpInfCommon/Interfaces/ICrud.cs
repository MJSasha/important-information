using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface ICrud<TEntity, TKey> where TEntity : class, IEntity
    {
        Task<List<TEntity>> Get();
        Task<TEntity> Get(TKey id);
        Task Post(TEntity item);
        Task Post(List<TEntity> item);
        Task Patch(TKey key, TEntity item);
        Task Delete(TKey key);
    }
}
