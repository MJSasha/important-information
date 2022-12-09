using ImpInfCommon.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ImpInfApi.Repository
{
    public class DaysRepository : BaseCrudRepository<Day>
    {
        private readonly DbSet<Day> dbSet;

        public DaysRepository(AppDbContext dbContext) : base(dbContext)
        {
            dbSet = dbContext.Set<Day>();
        }

        public override async Task<List<Day>> Read(Func<Day, bool> query = null, params Expression<Func<Day, object>>[] includedProperties)
        {
            var days = query != null ? dbSet.Include(d => d.LessonsAndTimes).ThenInclude(lt => lt.Lesson).Where(query).ToList() : dbSet.Include(d => d.LessonsAndTimes).ThenInclude(lt => lt.Lesson).ToList();
            if (!days.Any()) return days;
            foreach (var item in days)
            {
                item.LessonsAndTimes.ForEach(lt => lt.Days = null);
            }
            return days;
        }

        public override async Task<Day> ReadFirst(Expression<Func<Day, bool>> query, params Expression<Func<Day, object>>[] includedProperties)
        {
            var day = await dbSet.Include(d => d.LessonsAndTimes).ThenInclude(lt => lt.Lesson).FirstOrDefaultAsync(query);
            if (day == null) return day;
            day.LessonsAndTimes.ForEach(lt => lt.Days = null);
            return day;
        }
    }
}
