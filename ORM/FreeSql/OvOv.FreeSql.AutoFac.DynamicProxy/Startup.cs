using Autofac;
using AutoMapper;
using FreeSql;
using FreeSql.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OvOv.Core.Domain;
using OvOv.FreeSql.AutoFac.DynamicProxy.Repositories;
using OvOv.FreeSql.AutoFac.DynamicProxy.Services;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace OvOv.FreeSql.AutoFac.DynamicProxy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            IConfigurationSection Default = Configuration.GetSection("Default");
            IConfigurationSection SqlServer = Configuration.GetSection("SqlServer");
            IConfigurationSection MariaDB = Configuration.GetSection("MariaDB");

            Fsql = new FreeSqlBuilder()
                //.UseConnectionString(DataType.Sqlite, @"Data Source=|DataDirectory|\document.db;Pooling=true;Max Pool Size=10")
                .UseConnectionString(DataType.MySql, Default.Value)
                //.UseConnectionString(DataType.MySql, MariaDB.Value)
                //.UseConnectionString(DataType.SqlServer, SqlServer.Value)
                .UseAutoSyncStructure(true)
                .UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
                .UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
                .Build().SetDbContextOptions(opt => opt.EnableAddOrUpdateNavigateList = true);

            Fsql.Aop.CurdAfter += (s, e) =>
            {
                //Trace.WriteLine(
                //    $"ManagedThreadId:{Thread.CurrentThread.ManagedThreadId}: FullName:{e.EntityType.FullName}" +
                //    $" ElapsedMilliseconds:{e.ElapsedMilliseconds}ms, {e.Sql}");

                if (e.ElapsedMilliseconds > 200)
                {
                }
            };

            //Fsql.CodeFirst.Entity<Blog>(e =>
            //{

            //    e.HasData(new List<Blog>()
            //    {
            //            new Blog("title","content",DateTime.Now,false)
            //            {
            //                Posts=new List<Post>
            //                {
            //                    new Post("replyContent",DateTime.Now,false),
            //                    new Post("replyContent",DateTime.Now,false),
            //                    new Post("replyContent",DateTime.Now,false),
            //                }
            //            },
            //            new Blog("title","content",DateTime.Now,false)
            //            {
            //                Posts=new List<Post>
            //                {
            //                    new Post("replyContent",DateTime.Now,false),
            //                    new Post("replyContent",DateTime.Now,false),
            //                    new Post("replyContent",DateTime.Now,false),
            //                }
            //            }
            //    });
            //});

            //using Object<DbConnection> objPool = Fsql.Ado.MasterPool.Get();

            //using (DbConnection dbConnection = objPool.Value)
            //{
            //}

        }

        public IFreeSql Fsql { get; }
        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Fsql);
            services.AddScoped<UnitOfWorkManager>();
            services.AddFreeRepository(null, typeof(Startup).Assembly);

            Expression<Func<ISoftDelete, bool>> where = a => a.IsDeleted == false;
            Fsql.GlobalFilter.Apply("IsDeleted", where);

            services.AddScoped<TransBlogService>();

            services.AddControllersWithViews();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();

            services.AddAutoMapper(Assembly.Load("OvOv.Core"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "OvOv.FreeSql.Autofac.DynamicProxy", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OvOv.FreeSql.Autofac.DynamicProxy");
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
