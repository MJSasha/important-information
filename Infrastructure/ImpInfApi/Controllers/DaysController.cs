using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : BaseCrudController<Day>
    {
        private readonly BaseCrudRepository<Day> repository;
        private readonly BaseCrudRepository<LessonsAndTimes> ltRepository;

        public DaysController(BaseCrudRepository<Day> repository, BaseCrudRepository<LessonsAndTimes> ltRepository) : base(repository)
        {
            this.repository = repository;
            this.ltRepository = ltRepository;
        }

        [HttpPost("ByDates")]
        public Task<Day[]> GetByDates([FromBody] StartEndTime startEndTime)
        {
            return repository.Read(d => d.Date >= startEndTime.Start && d.Date <= startEndTime.End);
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

        public override async Task<ObjectResult> Post([FromBody] Day entity)
        {
            var lessonsAndTimes = await ltRepository.Read(lt => entity.LessonsAndTimes.Any(_lt => _lt.Type == lt.Type && _lt.LessonId == lt.LessonId && _lt.Time.TimeOfDay == lt.Time.TimeOfDay));
            foreach (var entityLessonsAndTimes in entity.LessonsAndTimes)
            {
                try
                {
                    entityLessonsAndTimes.Id = lessonsAndTimes.FirstOrDefault(lt => entityLessonsAndTimes.Type == lt.Type && entityLessonsAndTimes.LessonId == lt.LessonId && entityLessonsAndTimes.Time.TimeOfDay == lt.Time.TimeOfDay).Id;
                }
                catch (NullReferenceException)
                {
                    foreach (var _lessonsAndTimes in lessonsAndTimes)
                    {
                        entity.LessonsAndTimes.Add(_lessonsAndTimes);
                    }
                }
            }
            return await base.Post(entity);   
        }
    }
}
