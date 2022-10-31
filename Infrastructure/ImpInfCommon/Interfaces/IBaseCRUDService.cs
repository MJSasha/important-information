using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IBaseCRUDService<TEntity, TKey> : IBaseService
    {
        Task<IEntity> Get(TKey key);
        Task<List<TEntity>> Get();
        Task Create(TEntity item);
        Task Create(List<TEntity> item);
        Task Update(TKey key, TEntity item);
        Task Delete(TKey key);
        Task Delete(List<TKey> key);
    }
}
