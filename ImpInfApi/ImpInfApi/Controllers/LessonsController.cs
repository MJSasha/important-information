using ImpInfApi.Data.Entities;
using ImpInfApi.Services;
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
        public LessonsController(BaseCrudRepository<Lesson> repository) : base(repository, (Lesson lesson, int id) => lesson.Id == id)
        {
        }

        public override Task<ObjectResult> Patch(int id, [FromBody] Lesson lesson)
        {
            lesson.Id = id;
            return base.Patch(id, lesson);
        }
    }
}
