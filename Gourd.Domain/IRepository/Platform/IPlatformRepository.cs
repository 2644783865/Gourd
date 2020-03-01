using Gourd.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.IRepository.Platform
{
    public interface IPlatformRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

    }
}
