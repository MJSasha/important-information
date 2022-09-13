using ImpInfApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    public abstract class BaseCrudController<TEntity, TKey> : ControllerBase where TEntity : class
    {
        private readonly BaseCrudRepository<TEntity> repository;
        private readonly Func<TEntity, TKey, bool> idSearch;

        public BaseCrudController(BaseCrudRepository<TEntity> repository, Func<TEntity, TKey, bool> idSearch)
        {
            this.repository = repository;
            this.idSearch = idSearch;
        }

        [HttpGet]
        public Task<TEntity[]> Get()
        {
            return repository.Read();
        }

        [HttpGet("{id}")]
        public Task<TEntity> Get(TKey id)
        {
            return repository.ReadFirst(entity => idSearch(entity, id));
        }

        [HttpPost]
        public async Task<ObjectResult> Post([FromBody] TEntity entity)
        {
            await repository.Create(entity);
            return Ok("Entity create successful.");
        }

        [HttpPost("Many")]
        public async Task<ObjectResult> Post([FromBody] TEntity[] entities)
        {
            await repository.Create(entities);
            return Ok("Entities create successful.");
        }

        [HttpPatch("{id}")]
        public virtual async Task<ObjectResult> Patch(TKey id, [FromBody] TEntity entity)
        {
            await repository.Update(entity);
            return Ok("Update successful.");
        }

        [HttpDelete("{id}")]
        public async Task<ObjectResult> Delete(TKey id)
        {
            await repository.Delete(entity => idSearch(entity, id));
            return Ok("Delete successful.");
        }
    }
}
