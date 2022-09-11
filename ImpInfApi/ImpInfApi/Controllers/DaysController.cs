using ImpInfApi.Data.Entities;
using ImpInfApi.Data.Other;
using ImpInfApi.Data.SubModels;
using ImpInfApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DaysController : BaseCrudController<Day, int>
    {
        private readonly BaseCrudRepository<Day> repository;

        public DaysController(BaseCrudRepository<Day> repository) : base(repository, (Day day, int id) => day.Id == id)
        {
            this.repository = repository;
        }

        public override Task<ObjectResult> Patch(int id, [FromBody] Day day)
        {
            day.Id = id;
            return base.Patch(id, day);
        }

        [HttpPost("ByDates")]
        public Task<Day[]> GetByDates([FromBody] StartEndTime startEndTime)
        {
            return repository.Read(d => d.Date > startEndTime.Start && d.Date < startEndTime.End);
        }

        [HttpPost("ByDate")]
        public Task<Day> GetByDates([FromBody] DateTimeWrap dateWrap)
        {
            return repository.ReadFirst(d => d.Date.Day == dateWrap.DateTime.Day);
        }
    }
}
