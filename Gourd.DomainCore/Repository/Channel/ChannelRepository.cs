using Gourd.Domain.Entity;
using Gourd.Domain.IRepository;
using Gourd.Infrastructure.config;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.DomainCore.Repository.Channel
{
    public class ChannelRepository<T> : BaseRepository<T>, IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// 通过配置,构建上下文
        /// </summary>
        public ChannelRepository(IOptions<DBConfig> config) : base()
        {
            BaseDbContext dbContext = new ChannelDbContext(config.Value.ConnType, config.Value.ConnName);
            base.DbContent = dbContext;
            base.Table = this.DbContent.Set<T>();
        }
    }
}
