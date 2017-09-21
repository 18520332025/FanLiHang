using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mange.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using FanLiHang.Dapper.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using FanLiHang.Data;

namespace Mange
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IJWTAuth, JWTAuth>();
            // Add framework services.

            services.AddMvc();
            //缓存注册
            services.AddMemoryCache();

            //配置连接注册
            services.AddScoped<IDBConnectionStringConfig, DBConnectionStringConfig>(x => new DBConnectionStringConfig(Configuration));
            //配置数据访问器
            services.AddTransient<IDbConnection, System.Data.SqlClient.SqlConnection>();
            //配置DbHelper具体实现依赖缓存，数据访问器，连接访问器
            services.AddTransient<IDbHelper, DbHelper>();
            //配置数据处理
            ConfigureDBServices("FanLiHang.Data", services);
        }

        public void ConfigureDBServices(string dbDllName, IServiceCollection services)
        {
            //批量注入
            var assembly = System.Reflection.Assembly.Load(dbDllName);
            var types = assembly.GetTypes();
            foreach(var type in types)
            {
                if (type.IsClass)
                {
                    var interfaces = type.GetInterfaces();
                    if (interfaces.Count() == 1)
                    {
                        services.AddTransient(interfaces[0],type);
                    }
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseAuthentication();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "UserAuth",
                AutomaticAuthenticate = false,
                CookieHttpOnly = false
            });
        }

    }
}
