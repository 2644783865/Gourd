using Gourd.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.IRepository.Channel
{
    public interface IChannelRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

    }
}
