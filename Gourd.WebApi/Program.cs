using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gourd.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            var host = WebHost.CreateDefaultBuilder(args)
                           .ConfigureServices(services => services.AddAutofac())
                           .UseStartup<Startup>()
                           .Build();
            host.Run();

        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });


        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //   Host.CreateDefaultBuilder(args)
        //       .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //       .ConfigureWebHostDefaults(webBuilder =>
        //       {
        //           webBuilder
        //           .UseContentRoot(Directory.GetCurrentDirectory())
        //           .UseStartup<Startup>();
        //       });
    }
}
