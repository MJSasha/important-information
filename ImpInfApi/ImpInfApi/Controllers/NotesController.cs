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
    public class NotesController : BaseCrudController<Note, int>
    {
        public NotesController(BaseCrudRepository<Note> repository) : base(repository, (Note note, int id) => note.Id == id)
        {
        }

        public override Task<ObjectResult> Patch(int id, [FromBody] Note note)
        {
            note.Id = id;
            return base.Patch(id, note);
        }
    }
}
