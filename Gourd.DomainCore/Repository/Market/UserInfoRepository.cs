using Gourd.Domain.Entity.Market;
using Gourd.Domain.IRepository.Market;
using Gourd.Infrastructure.config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.DomainCore.Repository.Market
{
    public partial class UserInfoRepository : MarketRepository<UserInfo>
    {
        public UserInfoRepository(IOptions<DBConfig> config) : base(config)
        {

        }
    }
}
