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
    public class NotesController : BaseCrudController<Note, int>
    {
        private readonly BaseCrudRepository<Note> repository;

        public NotesController(BaseCrudRepository<Note> repository) : base(repository)
        {
            this.repository = repository;
        }

        public override Task<Note> Get(int id)
        {
            return repository.ReadFirst(note => note.Id == id);
        }

        public override Task<ObjectResult> Patch(int id, [FromBody] Note note)
        {
            note.Id = id;
            return base.Patch(id, note);
        }
    }
}
