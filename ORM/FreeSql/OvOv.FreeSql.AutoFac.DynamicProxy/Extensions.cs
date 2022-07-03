using FreeSql;
using FreeSql.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OvOv.Core.Domain;
using System.Diagnostics;

namespace OvOv.FreeSql.AutoFac.DynamicProxy
{
    public static class Extensions
    {
        public static IServiceCollection AddFreeSql(this IServiceCollection services, IConfiguration Configuration)
        {
            IConfigurationSection Default = Configuration.GetSection("Default");
            IConfigurationSection SqlServer = Configuration.GetSection("SqlServer");
            IConfigurationSection MariaDB = Configuration.GetSection("MariaDB");

            #region Fsql
            var Fsql = new FreeSqlBuilder()
                              //.UseConnectionString(DataType.Sqlite, @"Data Source=|DataDirectory|\document.db;Pooling=true;Max Pool Size=10")
                              .UseConnectionString(DataType.MySql, Default.Value)
                              //.UseConnectionString(DataType.MySql, MariaDB.Value)
                              //.UseConnectionString(DataType.SqlServer, SqlServer.Value)
                              .UseAutoSyncStructure(true)
                              .UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
                              .UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
                              .UseGenerateCommandParameterWithLambda(false)
                              .UseNoneCommandParameter(true)
                              .Build().SetDbContextOptions(opt => opt.EnableCascadeSave = true);

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
            #endregion

            Fsql.GlobalFilter.Apply<ISoftDelete>("IsDeleted", a => a.IsDeleted == false);

            services.AddSingleton(Fsql);
            services.AddFreeDbContext<OvOvDbContext>(options => options.UseFreeSql(Fsql));
            services.AddScoped<UnitOfWorkManager>();
            services.AddFreeRepository(null, typeof(Startup).Assembly);



            return services;
        }
    }
}
