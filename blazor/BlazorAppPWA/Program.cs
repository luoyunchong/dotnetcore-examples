using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazored.LocalStorage;
using System.IO;
using System.Reflection;

namespace BlazorAppPWA
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            string fileName = "BlazorAppPWA.appsettings.json";
            var stream = Assembly.GetExecutingAssembly()
                                 .GetManifestResourceStream(fileName);

            var config = new ConfigurationBuilder()
                     .AddJsonStream(stream)
                     .Build();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            //builder.Services.AddSingleton(fsql);
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddTransient(_ =>
            {
                return config.GetSection("GalaxyStuff")
                             .Get<GalaxyInfo>();
            });
            await builder.Build().RunAsync();

        }
    }
}
