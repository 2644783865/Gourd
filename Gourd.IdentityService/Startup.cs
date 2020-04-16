using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;

namespace Gourd.IdentityService
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var Assembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;


            //配置session的有效时间,单位秒
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString));

            //注入一个自定义的上下文
            services.AddDbContext<ConfigDbContext>(options =>
            options.UseMySql(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            var key = Configuration.GetValue<string>("key");

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.IssuerUri = "https://ids4.wmowm.com";
                options.PublicOrigin = "https://ids4.wmowm.com";
                
                options.UserInteraction = new UserInteractionOptions()
                {
                    ConsentReturnUrlParameter = "returnUrl"
                };
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseMySql(connectionString,
                        sql => sql.MigrationsAssembly(Assembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseMySql(connectionString,
                        sql => sql.MigrationsAssembly(Assembly));

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
            })
            .AddDeveloperSigningCredential()
            //.AddSigningCredential(new X509Certificate2("key/ids4.wm.crt",key))
            //.AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())
            //.AddInMemoryApiResources(InMemoryConfiguration.GetApiResources())
            //.AddInMemoryClients(InMemoryConfiguration.GetClients())
            //.AddAspNetIdentity<IdentityUser>();
            //.AddTestUsers(InMemoryConfiguration.GetUsers().ToList());

            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>(); //使用自定义校验

            //services.AddAuthentication();//配置认证服务

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    //options.Authority = "http://localhost:5000";
                    options.Authority = "http://localhost";  //容器内部使用80
                    options.RequireHttpsMetadata = false;
                    options.Audience = "webapi";
                    //options.ForwardSignIn = "http://localhost:5000/account/login";

                    options.ForwardSignIn = "http://localhost/account/login"; //容器内部使用80
                });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();//启动 IdentityServer


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });

            //InitializeDatabase(app);
        }
        private void InitializeDatabase(IApplicationBuilder app)
        {

            #region 读取初始化数据
            string userId = Guid.NewGuid().ToString();
            string roleId = Guid.NewGuid().ToString();
            List<SysUser> usereInfo = new List<SysUser>()
            {
                new SysUser()
                {
                    Id = userId,
                    Account = "tibos",
                    Name = "tibos",
                    Password = "123456"  //这里建议存MD5密文
                }
            };

            List<SysRole> SysRole = new List<SysRole>()
            {
                new SysRole()
                {
                    Id = roleId,
                    Memo = "管理员",
                    Name = "admin"
                }
            };

            List<SysUserRole> SysUserRole = new List<SysUserRole>()
            {
                new SysUserRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleId = roleId,
                    UserId = userId
                }
            };

            #endregion
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                    context.Database.Migrate();
                }
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                    context.Database.Migrate();
                    EnsureSeedData(context);
                }
                {
                    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();

                    var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    #region 用户信息导入
                    foreach (var user in usereInfo)
                    {
                        if (user == null || user.Account == null)
                        {
                            continue;
                        }
                        var userItem = userMgr.FindByNameAsync(user.Name).Result;
                        var rid = SysUserRole.FirstOrDefault(d => d.UserId == user.Id)?.RoleId;
                        var rNames = SysRole.Where(d => d.Id == rid).Select(d => d.Id).ToList();
                        var rName = SysRole.FirstOrDefault(d => d.Id == rid)?.Name;

                        if (userItem == null)
                        {
                            if (!string.IsNullOrEmpty(rid) && rNames.Count > 0)
                            {
                                #region identity默认的方式(反正我不用,麻烦的要死)
                                //userItem = new IdentityUser
                                //{
                                //    UserName = user.Name,
                                //};
                                //var pwdInit = "Abc@123";//初始密码
                                //var result = userMgr.CreateAsync(userItem, pwdInit).Result;
                                #endregion

                                #region 自定义用户
                                userItem = new IdentityUser
                                {
                                    UserName = user.Name,
                                    Email = "505613913@qq.com",
                                    PhoneNumber ="15019400000",
                                    PasswordHash = "Abc@123"
                                };
                                var result = userMgr.CreateAsync(userItem).Result;
                                #endregion
                                if (!result.Succeeded)
                                {
                                    throw new Exception(result.Errors.First().Description);
                                }
                                var claims = new List<Claim>{
                                    new Claim(JwtClaimTypes.Name, user.Name), //用户名
                                    new Claim(JwtClaimTypes.Email, userItem.Email),
                                    new Claim("rolename", rName),
                                };
                                claims.AddRange(rName.Select(s => new Claim(JwtClaimTypes.Role, s.ToString())));

                                result = userMgr.AddClaimsAsync(userItem, claims).Result;
                                if (!result.Succeeded)
                                {
                                    throw new Exception(result.Errors.First().Description);
                                }
                                Console.WriteLine($"{userItem?.UserName} created");//AspNetUserClaims 表
                            }
                            else
                            {
                                Console.WriteLine($"{user?.Account} doesn't have a corresponding role.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{userItem?.UserName} already exists");
                        }
                    }
                    #endregion

                    #region 角色信息导入
                    foreach (var role in SysRole)
                    {
                        if (role == null || role.Name == null)
                        {
                            continue;
                        }
                        var roleItem = roleMgr.FindByNameAsync(role.Name).Result;

                        if (roleItem != null)
                        {
                            role.Name = role.Name;
                        }

                        roleItem = new IdentityRole
                        {
                            Name = role.Name,
                        };
                        var result = roleMgr.CreateAsync(roleItem).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Console.WriteLine($"{roleItem?.Name} created");//AspNetUserClaims 表

                    }
                    #endregion
                }

            }
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                Console.WriteLine("Clients being populated");
                foreach (var client in InMemoryConfiguration.GetClients().ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Clients already populated");
            }

            if (!context.IdentityResources.Any())
            {
                Console.WriteLine("IdentityResources being populated");
                foreach (var resource in InMemoryConfiguration.GetIdentityResources().ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("IdentityResources already populated");
            }

            if (!context.ApiResources.Any())
            {
                Console.WriteLine("ApiResources being populated");
                foreach (var resource in InMemoryConfiguration.GetApiResources().ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("ApiResources already populated");
            }
        }

    }
}
