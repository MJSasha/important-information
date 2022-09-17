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
    public class NotesController : BaseCrudController<Note>
    {
        private readonly BaseCrudRepository<Note> repository;

        public NotesController(BaseCrudRepository<Note> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
