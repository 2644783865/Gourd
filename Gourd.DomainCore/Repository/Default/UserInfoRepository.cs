using Gourd.Domain.Entity.Default;
using Gourd.Domain.IRepository.Default;
using Gourd.Infrastructure.config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.DomainCore.Repository.Default
{
    public partial class UserInfoRepository : DefaultRepository<UserInfo>
    {
        public UserInfoRepository(IOptions<DBConfig> config) : base(config)
        {

        }
    }
}
