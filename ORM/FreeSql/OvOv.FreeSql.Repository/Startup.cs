using System.Diagnostics;
using System.Reflection;
using FreeSql;
using FreeSql.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using OvOv.FreeSql.Repository.Repositories;
using OvOv.FreeSql.Repository.Services;

namespace OvOv.FreeSql.Repository
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            string connectkey = Configuration.GetSection("ConnectString").Value;
            if (connectkey == "Mysql")
            {
                IConfigurationSection Mysql = Configuration.GetSection("Mysql");
                Fsql = new FreeSqlBuilder()
                    .UseConnectionString(DataType.MySql, Mysql.Value)
                    .UseAutoSyncStructure(true)
                    .UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
                    .UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
                    .Build();
            }
            else
            {
                IConfigurationSection MssqlConfiguration = Configuration.GetSection("MssqlServer");
                Fsql = new FreeSqlBuilder()
                                   .UseConnectionString(DataType.SqlServer, MssqlConfiguration.Value)
                                   .UseAutoSyncStructure(true)
                                   .UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
                                   .Build();
            }

            Fsql.CodeFirst.IsAutoSyncStructure = true;
        }

        public IFreeSql Fsql { get; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:MM:ss";


                // 返回Json属性命名默认按照小驼峰规则
                //opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); ;

                // 设置下划线方式，首字母是小写
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                    {
                        //ProcessDictionaryKeys = true
                    },
                };
            });

            services.AddSingleton(Fsql);
            services.AddScoped<UnitOfWorkManager>();
            services.AddFreeRepository(null, typeof(Startup).Assembly);
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<BlogService>();
            services.AddScoped<ITagService, TagService>();

            services.AddAutoMapper(Assembly.Load("OvOv.Core"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "OvOv.FreeSql.Repository", Version = "v1" });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OvOv.FreeSql.Repository");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
