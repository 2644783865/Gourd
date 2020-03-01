using Gourd.Domain.Entity.Platform;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.DomainCore.Repository.Platform
{
    public class PlatformDbContext : BaseDbContext
    {
        public PlatformDbContext(string connType, string connName) : base(connType, connName)
        {

        }
        public DbSet<Goods> Goods { get; set; }

        public DbSet<GoodsType> GoodsType { get; set; }
    }
}
