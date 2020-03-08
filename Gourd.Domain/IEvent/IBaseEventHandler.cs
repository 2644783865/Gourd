using Gourd.Domain.Dto;
using Gourd.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.Domain.IEvent
{
    public interface IBaseEventHandler<T> where T : BaseDto
    {
        public event EventHandler<T> BaseDomainEvent;

        void Run(T obj);

        Task RunAsync(T obj);
    }
}
