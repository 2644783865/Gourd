using Gourd.Domain.Entity;
using Gourd.Domain.IRepository;
using Gourd.Domain.IRepository.Market;
using Gourd.DomainCore.Repository.Market;
using Gourd.Infrastructure.config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.DomainCore.Repository.Market
{
    public class MarketRepository<T> : BaseRepository<T>, IMarketRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// 通过配置,构建上下文
        /// </summary>
        public MarketRepository(IOptions<DBConfig> config):base()
        {
            BaseDbContext dbContext = new MarketDbContext(config.Value.ConnType, config.Value.ConnName);
            base.DbContent = dbContext;
            base.Table = this.DbContent.Set<T>();
        }
    }
}
