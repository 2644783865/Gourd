using Gourd.Domain.Dto.Channel;
using Gourd.Domain.Entity.Channel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.DomainCore.Repository.Channel
{
    public class ChannelDbContext : BaseDbContext
    {
        public ChannelDbContext(string connType, string connName) : base(connType, connName)
        {

        }
        public DbSet<InventoryDto> Inventory { get; set; }
        public DbSet<OutboundRecordDto> OutboundRecord { get; set; }
    }
}
