using Gourd.Domain.Entity;
using Gourd.Domain.IRepository;
using Gourd.Domain.IRepository.Default;
using Gourd.DomainCore.Repository.Default;
using Gourd.Infrastructure.config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.DomainCore.Repository.Default
{
    public class DefaultRepository<T> : BaseRepository<T>, IDefaulttRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// 通过配置,构建上下文
        /// </summary>
        public DefaultRepository(IOptions<DBConfig> config):base()
        {
            BaseDbContext dbContext = new DefaultDbContext(config.Value.ConnType, config.Value.ConnName);
            base.DbContent = dbContext;
            base.Table = this.DbContent.Set<T>();
        }
    }
}
