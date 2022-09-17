using ImpInfApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    public abstract class BaseCrudController<TEntity, TKey> : ControllerBase where TEntity : class
    {
        private readonly BaseCrudRepository<TEntity> repository;

        public BaseCrudController(BaseCrudRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public Task<TEntity[]> Get()
        {
            return repository.Read();
        }

        [HttpGet("{id}")]
        public abstract Task<TEntity> Get(TKey id);

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

        [HttpDelete]
        public async Task<ObjectResult> Delete([FromBody] TEntity entity)
        {
            await repository.Delete(new[] { entity });
            return Ok("Delete successful.");
        }
    }
}
