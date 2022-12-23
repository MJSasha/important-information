using ImpInfApi.Repository;
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
    public class DaysController : BaseCrudController<Day>, IDaysService
    {
        private readonly BaseCrudRepository<Day> repository;
        private readonly BaseCrudRepository<LessonsAndTimes> ltRepository;

        public DaysController(BaseCrudRepository<Day> repository, BaseCrudRepository<LessonsAndTimes> ltRepository) : base(repository)
        {
            this.repository = repository;
            this.ltRepository = ltRepository;

            OnBeforePost += FixLTKeysInDay;
            OnBeforePatch += FixLTKeysInDay;
            OnBeforePostMany += async (days) => days.ForEach(async d => await FixLTKeysInDay(d));
        }

        [HttpPost("ByDates")]
        public Task<List<Day>> GetByDates([FromBody] StartEndTime startEndTime)
        {
            return repository.Read(d => d.Date >= startEndTime.Start && d.Date <= startEndTime.End);
        }

        [HttpPost("ByDate")]
        public Task<Day> GetByDate([FromBody] DateTimeWrap dateWrap)
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

        private async Task FixLTKeysInDay(Day day)
        {
            var lessonsAndTimes = await ltRepository.Read(lt => day.LessonsAndTimes.Any(_lt => _lt.Type == lt.Type && _lt.LessonId == lt.LessonId && _lt.Time.TimeOfDay == lt.Time.TimeOfDay));
            foreach (var entityLessonsAndTimes in day.LessonsAndTimes)
            {
                entityLessonsAndTimes.Id = lessonsAndTimes.FirstOrDefault(lt => entityLessonsAndTimes.Type == lt.Type && entityLessonsAndTimes.LessonId == lt.LessonId && entityLessonsAndTimes.Time.TimeOfDay == lt.Time.TimeOfDay)?.Id ?? default(int);
            }
        }
    }
}
