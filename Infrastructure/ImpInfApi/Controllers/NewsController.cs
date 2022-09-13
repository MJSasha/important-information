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
    public class NewsController : BaseCrudController<News, int>
    {
        private readonly BaseCrudRepository<News> repository;

        public NewsController(BaseCrudRepository<News> repository) : base(repository, (News news, int id) => news.Id == id)
        {
            this.repository = repository;
        }

        public override Task<ObjectResult> Patch(int id, [FromBody] News news)
        {
            news.Id = id;
            return base.Patch(id, news);
        }

        [HttpGet("Unsent")]
        public Task<News[]> GetUnsent()
        {
            return repository.Read(n => n.NeedToSend);
        }

        [HttpGet("ByLessonId/{lessonId}")]
        public Task<News[]> GetByLessonId(int lessonId)
        {
            return repository.Read(n => n.Lesson?.Id == lessonId, n => n.Lesson);
        }

        [HttpPost("ByDates")]
        public Task<News[]> GetByDates([FromBody] StartEndTime startEndTime)
        {
            return repository.Read(n => n.DateTimeOfCreate > startEndTime.Start && n.DateTimeOfCreate < startEndTime.End);
        }

        [HttpPost("AnyBefore")]
        public async Task<bool> CheckAnyNewsBefore([FromBody] DateTimeWrap date)
        {
            return (await repository.Read(n => n.DateTimeOfCreate < date.DateTime)).Any();
        }
    }
}
