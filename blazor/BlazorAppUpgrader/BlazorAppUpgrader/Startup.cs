using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// ** Upgrader Code
using System.IO;
using System.Runtime.Loader;
using Blazor.FileReader;
using System.Net.Http;

namespace BlazorAppUpgrader
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // ******
        // ** Upgrader Code (begin)
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            // Before we load the CustomClassLibrary.dll (and potentially lock it)
            // Determine if we have files in the Upgrade directory and process it first
            if (System.IO.File.Exists(env.ContentRootPath + @"\Upgrade\CustomClassLibrary.dll"))
            {
                // Delete current CustomClassLibrary.dll
                System.IO.File.Delete(env.ContentRootPath + @"\CustomModules\CustomClassLibrary.dll");

                // Copy new CustomClassLibrary.dll
                System.IO.File.Copy(
                    env.ContentRootPath + @"\Upgrade\CustomClassLibrary.dll",
                    env.ContentRootPath + @"\CustomModules\CustomClassLibrary.dll");

                // Delete Upgrade File - so it wont be processed again
                System.IO.File.Delete(env.ContentRootPath + @"\Upgrade\CustomClassLibrary.dll");
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        // ** Upgrader Code (end)
        // ******

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // ******
            // ** Upgrader Code (begin)

            // Load assembly from path
            // Note: The project that creates this assembly must reference
            // the parent project or the MVC framework features will not be 
            // 'found' when the code tries to run
            // This uses ApplicationParts
            // https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/app-parts
            // Also see: https://github.com/aspnet/Mvc/issues/4572
            var path = Path.GetFullPath(@"CustomModules\CustomClassLibrary.dll");
            var CustomClassLibrary = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);

            // Add framework services.
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddApplicationPart(CustomClassLibrary);

            // ** Upgrader Code (end)
            // ******

            services.AddRazorPages();
            services.AddServerSideBlazor();
            // ******
            // ** Upgrader Code (begin)

            services.AddScoped<HttpClient>();
            services.AddFileReaderService(options => options.InitializeOnFirstCall = true);
            services.AddServerSideBlazor().AddHubOptions(o =>
            {
                o.MaximumReceiveMessageSize = 10 * 1024 * 1024;
            });

            // ** Upgrader Code (end)
            // ******
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");

                // ******
                // ** Upgrader Code (begin)
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                // ** Upgrader Code (end)
                // ******
            });
        }
    }
}
