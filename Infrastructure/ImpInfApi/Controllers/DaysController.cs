using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DaysController : BaseCrudController<Day>
    {
        private readonly BaseCrudRepository<Day> repository;

        public DaysController(BaseCrudRepository<Day> repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("ByDates")]
        public Task<Day[]> GetByDates([FromBody] StartEndTime startEndTime)
        {
            return repository.Read(d => d.Date > startEndTime.Start && d.Date < startEndTime.End);
        }

        [HttpPost("ByDate")]
        public Task<Day> GetByDates([FromBody] DateTimeWrap dateWrap)
        {
            return repository.ReadFirst(d => d.Date == dateWrap.DateTime.Date);
        }

        [HttpPost("AnyBefore")]
        public async Task<bool> AnyBefore([FromBody] DateTimeWrap date)
        {
            return (await repository.Read(d => d.Date < date.DateTime.Date)).Any();
        }

        [HttpPost("AnyAfter")]
        public async Task<bool> AnyAfter([FromBody] DateTimeWrap date)
        {
            return (await repository.Read(d => d.Date > date.DateTime.Date)).Any();
        }
    }
}
