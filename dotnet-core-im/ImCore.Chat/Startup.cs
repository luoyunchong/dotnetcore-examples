using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ImCore.Chat
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
            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = " API 文档",
                    Description = "by bj eland"
                });

                options.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImCore.Chat.xml"));

            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                c.DocExpansion(DocExpansion.None);
            });
            ImHelper.Initialization(new ImClientOptions
            {
                Redis = new CSRedis.CSRedisClient("127.0.0.1:6379,poolsize=5"),
                Servers = new[] { "127.0.0.1:6001" }
            });


            ImHelper.Instance.OnSend += (s, e) =>
                Console.WriteLine($"ImClient.SendMessage(server={e.Server},data={JsonConvert.SerializeObject(e.Message)})");

            ImHelper.EventBus(
                t =>
                {
                    Console.WriteLine(t.clientId + "上线了");
                    var onlineUids = ImHelper.GetClientListByOnline();
                    ImHelper.SendMessage(t.clientId, onlineUids, $"用户{t.clientId}上线了");
                },
                t => Console.WriteLine(t.clientId + "下线了"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
