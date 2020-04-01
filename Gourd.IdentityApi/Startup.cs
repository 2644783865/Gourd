using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gourd.IdentityApi.Model;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gourd.IdentityApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //Ȩ����֤
            var identityServerOptions = new IdentityServerOptions()
            {
                IdentityScheme = "Bearer",
                ServerIP = "localhost",
                ServerPort = 5000,
                ResourceName = "CCC"
            };
            Configuration.Bind("IdentityServerOptions", identityServerOptions);
            services.AddMvcCore().AddAuthorization();
            services.AddAuthentication(identityServerOptions.IdentityScheme)
                .AddIdentityServerAuthentication(identityServerOptions.IdentityScheme, options =>
                {
                    options.RequireHttpsMetadata = false; //�Ƿ�����https
                    options.Authority = $"http://{identityServerOptions.ServerIP}:{identityServerOptions.ServerPort}";//������Ȩ��֤�ĵ�ַ
                    options.ApiName = identityServerOptions.ResourceName; //��Դ���ƣ�����֤������ע�����Դ�б������е�apiResourceһ��
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.ApiSecret = "123456";

                    //options.Authentication.Schemes = AuthenticationSchemes.NTLM;
                }
                );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
