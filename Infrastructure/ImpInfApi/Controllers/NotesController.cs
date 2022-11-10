using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImpInfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : BaseCrudController<Note>, INotes
    {
        public NotesController(BaseCrudRepository<Note> repository) : base(repository) { }
    }
}
