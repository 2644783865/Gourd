using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Reflection;
using Gourd.Domain.IRepository.Market;
using Gourd.DomainCore.Repository.Market;
using Gourd.Domain.Entity.Market;

namespace Gourd.WebApi.autofac
{
    public class DefaultModule : Autofac.Module
    {
        //Autofac容器
        protected override void Load(ContainerBuilder builder)
        {


            //拦截器
            //builder.Register(c => new AopRedis());

            //注入类
            //builder.RegisterType<OrdersService>().As<IOrdersService>().PropertiesAutowired().EnableInterfaceInterceptors();


            //程序集注入
            var Domain = Assembly.Load("Gourd.Domain");
            var DomainCore = Assembly.Load("Gourd.DomainCore");

            var Application = Assembly.Load("Gourd.Application");



            //根据名称约定（仓储层的接口和实现均以Repository结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(Domain, DomainCore)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Domain, DomainCore)
              .Where(t => t.Name.EndsWith("Domain"))
              .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Domain, DomainCore)
              .Where(t => t.Name.EndsWith("Facory"))
              .AsImplementedInterfaces().InstancePerLifetimeScope();

            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(Application)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces().InstancePerLifetimeScope();

            //切面只能使用瞬态生命周期
            //builder.RegisterType<RedisService>().As<IRedisService>().EnableInterfaceInterceptors();


            // builder.RegisterType<UserInfoRepository>().As<IMarketRepository<UserInfo>>().PropertiesAutowired();
        }
    }
}
