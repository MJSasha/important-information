using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return repository.ReadFirst(d => d.Date.Day == dateWrap.DateTime.Day);
        }
    }
}
