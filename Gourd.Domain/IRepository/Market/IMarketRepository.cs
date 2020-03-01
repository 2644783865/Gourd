using Gourd.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.IRepository.Market
{
    public interface IMarketRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

    }
}
