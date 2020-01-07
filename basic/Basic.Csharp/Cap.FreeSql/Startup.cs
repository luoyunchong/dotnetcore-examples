using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Savorboard.CAP.InMemoryMessageQueue;

namespace Cap.FreeSql
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Fsql = new FreeSqlBuilder()
                .UseConnectionString(DataType.MySql, @"Data Source=127.0.0.1;Port=3306;User ID=root;Password=123456;Initial Catalog=FreeSql;Charset=utf8;SslMode=none;Max pool size=10")
                .UseAutoSyncStructure(true)
                .Build();
        }

        public IConfiguration Configuration { get; }
        public IFreeSql Fsql { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFreeSql>(Fsql);

            services.AddCap(x =>
            {
                //x.UseInMemoryStorage();
                x.UseMySql(
                    "Data Source=localhost;Port=3306;User ID=root;Password=123456;Initial Catalog=FreeSql;Charset=utf8mb4;SslMode=none;Max pool size=10");
                x.UseInMemoryMessageQueue();
                x.UseDashboard();
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
