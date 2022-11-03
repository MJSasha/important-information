using ImpInfApi.Repository;
using ImpInfCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    public abstract class BaseCrudController<TEntity> : ControllerBase, ICrud<TEntity> where TEntity : class, IEntity 
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
        public virtual async Task<ObjectResult> Post([FromBody] TEntity entity)
        {
            await repository.Create(entity);
            return Ok("Entity create successful.");
        }

        [HttpPost("Many")]
        public virtual async Task<ObjectResult> Post([FromBody] TEntity[] entities)
        {
            await repository.Create(entities);
            return Ok("Entities create successful.");
        }

        [HttpPatch("{id}")]
        public virtual async Task<ObjectResult> Patch(int id, [FromBody] TEntity entity)
        {
            entity.Id = id;
            await repository.Update(entity);
            return Ok("Update successful.");
        }

        [HttpDelete("{id}")]
        public virtual async Task<ObjectResult> Delete(int id)
        {
            await repository.Delete(id);
            return Ok("Delete successful.");
        }
    }
}
