using Gourd.Domain.Entity;
using Gourd.Domain.IRepository;
using Gourd.DomainCore.Repository.Market;
using Gourd.Infrastructure.config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.DomainCore.Repository.Platform
{
    public class PlatformRepository<T> : BaseRepository<T>, IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// 通过配置,构建上下文
        /// </summary>
        public PlatformRepository(IOptions<DBConfig> config) : base()
        {
            BaseDbContext dbContext = new PlatformDbContext(config.Value.ConnType, config.Value.ConnName);
            base.DbContent = dbContext;
            base.Table = this.DbContent.Set<T>();
        }
    }
}
