using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LessonsController : BaseCrudController<Lesson, int>
    {
        private readonly BaseCrudRepository<Lesson> repository;

        public LessonsController(BaseCrudRepository<Lesson> repository) : base(repository)
        {
            this.repository = repository;
        }

        public override Task<Lesson> Get(int id)
        {
            return repository.ReadFirst(lesson => lesson.Id == id);
        }

        public override Task<ObjectResult> Patch(int id, [FromBody] Lesson lesson)
        {
            lesson.Id = id;
            return base.Patch(id, lesson);
        }
    }
}
