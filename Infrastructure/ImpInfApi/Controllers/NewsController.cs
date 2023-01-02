using ImpInfApi.Repository;
using ImpInfApi.Services;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using ImpInfCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : BaseCrudController<News>, INewsService
    {
        private readonly NewsRepository repository;
        private readonly NotificationsService notificationsService;

        public NewsController(NewsRepository repository, NotificationsService notificationsService) : base(repository)
        {
            this.repository = repository;
            this.notificationsService = notificationsService;
        }


        public override async Task Post([FromBody] News entity)
        {
            await base.Post(entity);
            await notificationsService.NotifyNewsCreated(entity);
        }


        public override Task<List<News>> Get()
        {
            return repository.Read(includedProperties: n => n.Lesson);
        }

        public override Task<News> Get(int id)
        {
            return repository.ReadFirst(entity => entity.Id == id, n => n.Lesson);
        }

        [HttpGet("Unsent")]
        public Task<List<News>> GetUnsent()
        {
            return repository.Read(n => n.NeedToSend);
        }

        [HttpGet("ByLessonId/{lessonId}")]
        public Task<List<News>> GetByLessonId(int lessonId)
        {
            return repository.Read(n => n.Lesson?.Id == lessonId, n => n.Lesson);
        }

        [HttpPost("ByDates")]
        public Task<List<News>> GetByDates([FromBody] StartEndTime startEndTime)
        {
            return repository.Read(n => n.DateTimeOfCreate > startEndTime.Start && n.DateTimeOfCreate < startEndTime.End);
        }

        [HttpGet("ReadIntervalSortedByDate")]
        public Task<List<News>> ReadIntervalSortedByDate(int start, int take)
        {
            return repository.ReadWithOrderByDate(start, take); //TODO - include Users
        }

        [HttpPost("AnyBefore")]
        public async Task<bool> CheckAnyNewsBefore([FromBody] DateTimeWrap date)
        {
            return (await repository.Read(n => n.DateTimeOfCreate.Date < date.DateTime.Date)).Any();
        }
    }
}
