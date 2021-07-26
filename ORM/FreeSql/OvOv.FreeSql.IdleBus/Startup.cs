using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FreeSql;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace OvOv.FreeSql.IdleBus
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
            var fsql = new MultiFreeSql();
            fsql.Register("db1", () =>
            {
                var db = new FreeSqlBuilder()
                          .UseConnectionString(DataType.MySql, "Data Source=127.0.0.1;Port=3306;User ID=root;Password=root; Initial Catalog=db1;Charset=utf8; SslMode=none;Min pool size=1")
                          .UseMonitorCommand(cmd => Console.WriteLine($"Ïß³Ì£º{cmd.CommandText}\r\n"))
                           .UseNoneCommandParameter(true)
                          .UseAutoSyncStructure(true)
                          .Build();

                db.GlobalFilter
                .Apply<IDeleted>("test4", r => r.IsDeleted == false);

                return db;
            }
         );

            fsql.Register("db2", () =>
            {
                var db = new FreeSqlBuilder()
                          .UseConnectionString(DataType.MySql, "Data Source=127.0.0.1;Port=3306;User ID=root;Password=root; Initial Catalog=db2;Charset=utf8; SslMode=none;Min pool size=1")
                          .UseAutoSyncStructure(true)
                          .Build();
                return db;
            }
        );
            fsql.Register("db3", () =>
            {
                var db = new FreeSqlBuilder()
                          .UseConnectionString(DataType.MySql, "Data Source=127.0.0.1;Port=3306;User ID=root;Password=root; Initial Catalog=db3;Charset=utf8; SslMode=none;Min pool size=1")
                          .UseAutoSyncStructure(true)
                          .Build();
                return db;
            }
        );



            services.AddSingleton<IFreeSql>(fsql);
            services.AddFreeRepository();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OvOv.FreeSql.IdleBus", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OvOv.FreeSql.IdleBus v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
