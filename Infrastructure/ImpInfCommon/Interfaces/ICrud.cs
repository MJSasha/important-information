using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface ICrud<TEntity, Tkey> where TEntity : class, IEntity
    {
        Task<List<TEntity>> Get();
        Task<TEntity> Get(Tkey id);

        // Api BaseCrudController:
        //Task<ObjectResult> Post(TEntity entity);
        //Task<ObjectResult> Post(TEntity[] entities);
        //Task<ObjectResult> Patch(int id, TEntity entity);
        //Task<ObjectResult> Delete(int id);

        // BaseCRUDService methods:
        //Task Create(TEntity item);
        //Task Create(List<TEntity> item);
        //Task Update(TKey key, TEntity item);
        //Task Update(TKey key, TEntity item);
        //Task Delete(List<TKey> key);
    }
}
