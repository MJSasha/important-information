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
    public class LessonsController : BaseCrudController<Lesson>
    {
        private readonly BaseCrudRepository<Lesson> repository;

        public LessonsController(BaseCrudRepository<Lesson> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
