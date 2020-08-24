using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static BlazorApp1.Pages.Index;
using System.Diagnostics;
using Newtonsoft.Json;

namespace BlazorApp1
{
    public class Program
    {
 
        public static async Task Main(string[] args)
        { 


            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(new HttpClient{BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            await builder.Build().RunAsync();
        }

        

    }
}
