using ImpInfApi.Repository;
using ImpInfCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    public abstract class BaseCrudController<TEntity> : ControllerBase, ICrudService<TEntity, int> where TEntity : class, IEntity
    {
        protected Func<List<TEntity>, Task> OnAfterGetMany;
        protected Func<TEntity, Task> OnAfterGet;

        protected Func<List<TEntity>, Task> OnAfterPostMany;
        protected Func<TEntity, Task> OnAfterPost;
        protected Func<List<TEntity>, Task> OnBeforePostMany;
        protected Func<TEntity, Task> OnBeforePost;

        protected Func<TEntity, Task> OnBeforePatch;
        protected Func<TEntity, Task> OnAfterPatch;

        private readonly BaseCrudRepository<TEntity> repository;

        public BaseCrudController(BaseCrudRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual async Task<List<TEntity>> Get()
        {
            var entities = await repository.Read();
            if (OnAfterGet != null) await OnAfterGetMany(entities);
            return entities;
        }

        [HttpGet("{id}")]
        public virtual async Task<TEntity> Get(int id)
        {
            var entity = await repository.ReadFirst(entity => entity.Id == id);
            if (OnAfterGet != null) await OnAfterGet(entity);
            return entity;
        }

        [HttpPost]
        public virtual async Task Post([FromBody] TEntity entity)
        {
            if (OnBeforePost != null) await OnBeforePost(entity);
            await repository.Create(entity);
            if (OnAfterPost != null) await OnAfterPost(entity);
        }

        [HttpPost("Many")]
        public virtual async Task Post([FromBody] List<TEntity> entities)
        {
            if (OnBeforePostMany != null) await OnBeforePostMany(entities);
            await repository.Create(entities);
            if (OnAfterPostMany != null) await OnAfterPostMany(entities);
        }

        [HttpPatch("{id}")]
        public virtual async Task Patch(int id, [FromBody] TEntity entity)
        {
            if (OnBeforePatch != null) await OnBeforePatch(entity);
            entity.Id = id;
            await repository.Update(entity);
            if (OnAfterPatch != null) await OnAfterPatch(entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task Delete(int id)
        {
            await repository.Delete(id);
        }
    }
}
