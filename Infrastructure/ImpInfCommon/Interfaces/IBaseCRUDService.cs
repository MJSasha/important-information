using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface ICrud<TEntity>
    {
        Task<List<TEntity>> Get();
        Task<TEntity> Get(int id);
        //Task<ObjectResult> Post(TEntity entity);
        //Task<ObjectResult> Post(TEntity[] entities);
        //Task<ObjectResult> Patch(int id, TEntity entity);
        //Task<ObjectResult> Delete(int id);
    }
}
