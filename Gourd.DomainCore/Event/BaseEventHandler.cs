using AutoMapper;
using Gourd.Domain.Dto;
using Gourd.Domain.IEvent;
using Gourd.Domain.Model;
using Gourd.Infrastructure.config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.DomainCore.Event
{
    public class BaseEventHandler<T>: IBaseEventHandler<T> where T:BaseDto
    {
        public event EventHandler<T> BaseDomainEvent;

        public void Run(T model)
        {
            BaseDomainEvent?.Invoke(this, model);
        }

        public Task RunAsync(T model)
        {
            return Task.Run(() =>
            {
                Run(model);
            });
        }
    }
}
