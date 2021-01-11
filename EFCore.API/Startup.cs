using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using EFCore.BLL.IService;
using EFCore.BLL.Service;
using EFCore.DAL.Common.Core;
using EFCore.DAL.Common.Interface;
using EFCore.Tools.Cache;
using EFCore.Tools.Cache.Service;
using EFCore.Tools.Extensions;
using EFCore.Tools.Helpers;
using EFCore.Tools.Ioc;
using EFCore.Tools.LogManange;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace EFCore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private readonly string Any = "Any";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Json 序列化中文乱码
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });

            // 配置EF服务注册
            services.AddDbContextPool<ConCardContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("sqlserverstring")));

            //依赖注入
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<IconCardContext, ConCardContext>();
            
            //注入程序集
            services.AddScopedAssembly("EFCore.BLL");

            //注册跨域
            services.AddCors(options =>
            {
                options.AddPolicy(Any, corsbuilder =>
                {
                    var corsPath = Configuration.GetSection("CorsPaths").GetChildren().Select(p => p.Value).ToArray();
                    corsbuilder.WithOrigins(corsPath)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });


            //注册Swagger生成器，定义一个或多个Swagger文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    //OpenAPI文档的版本
                    Version = "v1",
                    //标题
                    Title = "ToDo API",
                    //描述
                    Description = "一个简单的例子ASP.NET核心Web API",
                    //API服务条款的URL。必须采用URL格式。
                    TermsOfService = new Uri("https://example.com/terms"),
                    //暴露的API的联系信息。
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    //公开的API的许可证信息。
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "EFCore.API.xml");
                c.IncludeXmlComments(xmlPath);
            });

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


            //生成Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,//是否验证Issuer
                         ValidateAudience = true,//是否验证Audience
                         ValidateLifetime = true,//是否验证失效时间
                         ValidateIssuerSigningKey = true,//是否验证SecurityKey                       
                         AudienceValidator = (m, n, z) => { return m != null && m.FirstOrDefault().Equals(Configuration["ValidAudience"]); },//这里采用动态验证的方式，在重新登陆时，刷新token，旧token就强制失效了
                         ValidIssuer = "igbom_web",//Issuer，这两项和前面签发jwt的设置一致
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))//拿到SecurityKey
                     };
                 });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //启用jwt验证
            app.UseAuthentication();

            //访问HTML 静态页面
            app.UseStaticFiles();

            //初始化日志
            LogHelper.Configure();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //启用中间件为生成的 JSON 文档和 Swagger UI 提供服务

            //使中间件能够将生成的Swagger作为JSON端点提供服务
            app.UseSwagger(x =>
            {
                x.SerializeAsV2 = true;
            });

            //使中间件能够服务于swagger ui(HTML、JS、CSS等)
            //指定Swagger JSON端点。
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection(); 
            app.UseRouting();
            app.UseCors(Any);
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
              
            });
        }
    }
}
