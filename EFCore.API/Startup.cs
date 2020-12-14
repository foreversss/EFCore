using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            //Json ���л���������
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });

            // ����EF����ע��
            services.AddDbContext<ConCardContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("sqlserverstring")));

            //����ע��
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<IconCardContext, ConCardContext>();
            
            //ע�����
            services.AddScopedAssembly("EFCore.BLL");

            //ע�����
            services.AddCors(x => x.AddPolicy(Any, a => a.SetIsOriginAllowed
                (_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));


            //ע��Swagger������������һ������Swagger�ĵ�
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    //OpenAPI�ĵ��İ汾
                    Version = "v1",
                    //����
                    Title = "ToDo API",
                    //����
                    Description = "һ���򵥵�����ASP.NET����Web API",
                    //API���������URL���������URL��ʽ��
                    TermsOfService = new Uri("https://example.com/terms"),
                    //��¶��API����ϵ��Ϣ��
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    //������API�����֤��Ϣ��
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
                var xmlPath = Path.Combine(basePath, "EFCore.API.xml");
                c.IncludeXmlComments(xmlPath);
            });

            //ע��MemoryCache����
            services.AddMemoryCache();

            //��ȡ��������
            CacheProvider.cacheProvider.InitConnect(Configuration);

            //�ж��Ƿ�ʹ��Redis����
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
            //����HTML ��̬ҳ��
            app.UseStaticFiles();

            //��ʼ����־
            LogHelper.Configure();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //�����м��Ϊ���ɵ� JSON �ĵ��� Swagger UI �ṩ����

            //ʹ�м���ܹ������ɵ�Swagger��ΪJSON�˵��ṩ����
            app.UseSwagger(x =>
            {
                x.SerializeAsV2 = true;
            });

            //ʹ�м���ܹ�������swagger ui(HTML��JS��CSS��)
            //ָ��Swagger JSON�˵㡣
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
