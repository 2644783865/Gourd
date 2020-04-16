using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using IdentityModel;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Gourd.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //nginx 
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("admin.wmowm.com");
            });

            //此处可以不指定,默认值如此
            services.AddHttpsRedirection(options => {
                options.HttpsPort = 5001; //本地环境请使用5001
                options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect; //强制重定向,本地环境注释掉
            });
            services.AddMvc();


            services.AddSameSiteCookiePolicy();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddControllersWithViews();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // ensure not change any return Claims from Authorization Server



            //添加认证服务，并设置其有关选项
            services.AddAuthentication(options =>
            {
                // 客户端应用设置使用"Cookies"进行认证
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                // identityserver4设置使用"oidc"进行认证
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                 // 对使用的OpenIdConnect进行设置，此设置与Identityserver的config.cs中相应client配置一致才可能登录授权成功
                 .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                 {
                     options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                     options.SignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                     options.Authority = "https://ids4.wmowm.com";
                     options.RequireHttpsMetadata = false;//必须https协议
                     options.ClientId = "mvc";//idp项目中配置的client
                     options.ClientSecret = "111111";
                     options.SaveTokens = true;

                     // 下边是所有的scope,必须要和idp项目中一致,至少是一部分
                     //options.Scope.Clear();
                     //options.Scope.Add("roles");//"roles"
                     //options.Scope.Add("rolename");//"rolename"
                     //options.Scope.Add(OidcConstants.StandardScopes.OpenId);//"openid"
                     //options.Scope.Add(OidcConstants.StandardScopes.Profile);//"profile"
                     //options.Scope.Add(OidcConstants.StandardScopes.Email);//"email"
                 });


           // services.AddAuthentication(options =>
           // {
           //     options.DefaultScheme = "Cookies";
           //     options.DefaultChallengeScheme = "oidc"; // oidc => open ID connect
                
           // })
           // .AddCookie("Cookies")
           //.AddOpenIdConnect("oidc", options =>
           //{
           //    options.Authority = "https://ids4.wmowm.com";
           //    options.RequireHttpsMetadata = false;
           //    options.ClientId = "mvc";
           //    options.SaveTokens = true;
           //});

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired().InstancePerDependency();
            //builder.RegisterModule<DefaultModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHsts();
            app.UseForwardedHeaders();
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
