using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImpInfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : BaseCrudController<Lesson>, ILessonService
    {
        private readonly BaseCrudRepository<Lesson> repository;

        public LessonsController(BaseCrudRepository<Lesson> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
