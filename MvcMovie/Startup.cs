using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.DAL.Common.Core;
using EFCore.DAL.Common.Interface;
using EFCore.Tools.Cache;
using EFCore.Tools.Cache.Service;
using EFCore.Tools.Extensions;
using EFCore.Tools.LogManange;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcMovie
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
            services.AddControllersWithViews();

            // 配置EF服务注册
            services.AddDbContext<ConCardContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("sqlserverstring")));

            //依赖注入
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<IconCardContext, ConCardContext>();

            //注入程序集
            services.AddScopedAssembly("EFCore.BLL");

            //注入MemoryCache缓存
            services.AddMemoryCache();

            //获取缓存属性
            CacheProvider.cacheProvider.InitConnect(Configuration);

            //判断是否使用Redis缓存
            if (CacheProvider.cacheProvider._isUseRedis)
            {
                services.AddSingleton(typeof(IMemoryCacheService), new RedisCacheService(new RedisCacheOptions()
                {
                    Configuration = CacheProvider.cacheProvider._connectionString,
                    InstanceName = CacheProvider.cacheProvider._instanceName
                }, 0));
            }
            else
            {
                services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            }

            //注册身份验证
            services.AddAuthentication("Cookies").AddCookie("Cookies");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //初始化日志
            LogHelper.Configure();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
