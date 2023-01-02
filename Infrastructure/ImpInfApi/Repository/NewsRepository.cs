using ImpInfCommon.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ImpInfApi.Repository
{
    public class NewsRepository : BaseCrudRepository<News>
    {
        private readonly AppDbContext dbContext;

        public NewsRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<News>> ReadWithOrderByDate(int start, int take, params Expression<Func<News, object>>[] includedProperties)
        {
            var news = IncludProperties(includedProperties).OrderBy(n => n.DateTimeOfCreate).Skip(start).Take(take).ToList();
            foreach (var item in news)
            {
                dbContext.Entry(item).State = EntityState.Detached;
            }
            return Task.FromResult(news);
        }
    }
}
