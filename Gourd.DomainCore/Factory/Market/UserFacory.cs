using Gourd.Domain.Dto.Market;
using Gourd.Domain.Entity.Market;
using Gourd.Domain.IFactory.Market;
using Gourd.DomainCore.Repository.Market;
using Gourd.Infrastructure;
using Gourd.Infrastructure.config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.DomainCore.Factory.Market
{
    public class UserFacory: IUserFacory
    {
        private readonly DBConfig _config;
        public UserFacory(IOptions<DBConfig> config)
        {
            _config = config.Value;
        }
       
    }
}
