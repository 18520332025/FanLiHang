using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FanLiHang.Admins.Data;
using FanLiHang.Admins.Models;
using FanLiHang.Admins.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using FanLiHang.Dapper.Helper;
using System.Data;
using FanLiHang.Admins.Auth;
using Microsoft.Extensions.Caching.Memory;

namespace FanLiHang.Admins
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
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            //// Add application services.
            //services.AddTransient<IEmailSender, EmailSender>();
            services.AddMvc();

            //缓存注册
            services.AddMemoryCache();
            //认证配置
            services.AddScoped<IJWTAuth, JWTAuth>();
            //配置连接注册
            services.AddScoped<IDBConnectionStringConfig, DBConnectionStringConfig>(x => new DBConnectionStringConfig(Configuration));
            //配置数据访问器
            services.AddTransient<IDbConnection, System.Data.SqlClient.SqlConnection>();
            //配置DbHelper具体实现依赖缓存，数据访问器，连接访问器
            services.AddTransient<IDbHelper, DbHelper>();
            //配置数据处理
            ConfigureDBServices("FanLiHang.Data", services);

            services.AddAuthentication(option => { option.DefaultAuthenticateScheme = "UserAuth"; })
                .AddCookie("UserAuth", x =>
                {
                    x.Cookie.HttpOnly = false;
                    x.Cookie.Path = "/";
                    x.LoginPath = "/login/admin";
                });
        }

        public void ConfigureDBServices(string dbDllName, IServiceCollection services)
        {
            //批量注入
            var assembly = System.Reflection.Assembly.Load(dbDllName);
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsClass)
                {
                    var interfaces = type.GetInterfaces();
                    if (interfaces.Count() == 1)
                    {
                        services.AddScoped(interfaces[0], type);
                    }
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Admin}/{action=Index}/{id?}");
            });
        }
    }
}
