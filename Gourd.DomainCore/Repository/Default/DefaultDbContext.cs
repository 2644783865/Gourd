using Gourd.Domain.Entity.Default;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.DomainCore.Repository.Default
{
    public class DefaultDbContext : BaseDbContext
    {
        public DefaultDbContext(string connType, string connName) : base(connType, connName)
        {

        }
        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<UserLogin> UserLogin { get; set; }

        public DbSet<UserAccount> UserAccount { get; set; }
    }
}
