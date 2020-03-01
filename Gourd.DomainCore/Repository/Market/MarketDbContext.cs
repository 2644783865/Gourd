using Gourd.Domain.Entity.Market;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.DomainCore.Repository.Market
{
    public class MarketDbContext : BaseDbContext
    {
        public MarketDbContext(string connType, string connName) : base(connType, connName)
        {

        }
        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<UserLogin> UserLogin { get; set; }
    }
}
