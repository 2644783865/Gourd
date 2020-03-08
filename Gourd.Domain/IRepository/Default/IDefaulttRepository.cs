using Gourd.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Domain.IRepository.Default
{
    public interface IDefaulttRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

    }
}
