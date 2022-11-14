using ImpInfApi.Repository;
using ImpInfCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    public abstract class BaseCrudController<TEntity> : ControllerBase, ICrud<TEntity, int> where TEntity : class, IEntity
    {
        private readonly BaseCrudRepository<TEntity> repository;

        public BaseCrudController(BaseCrudRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual Task<List<TEntity>> Get()
        {
            return repository.Read();
        }

        [HttpGet("{id}")]
        public virtual Task<TEntity> Get(int id)
        {
            return repository.ReadFirst(entity => entity.Id == id);
        }

        [HttpPost]
        public virtual async Task Post([FromBody] TEntity entity)
        {
            await repository.Create(entity);
        }

        [HttpPost("Many")]
        public virtual async Task Post([FromBody] List<TEntity> entities)
        {
            await repository.Create(entities);
        }

        [HttpPatch("{id}")]
        public virtual async Task Patch(int id, [FromBody] TEntity entity)
        {
            entity.Id = id;
            await repository.Update(entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task Delete(int id)
        {
            await repository.Delete(id);
        }
    }
}
