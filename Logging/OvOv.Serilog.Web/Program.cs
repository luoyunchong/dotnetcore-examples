using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace OvOv.Serilog
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                       .MinimumLevel.Debug()
                       .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                       .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                       .Enrich.FromLogContext()
                       .WriteTo.Console()
                       .WriteTo.Debug(new RenderedCompactJsonFormatter())
                       .WriteTo.MySQL("server=127.0.0.1;uid=root;pwd=123456;database=logging_database;")
                       .CreateLogger();

            Log.Information("This informational message will be written to MySQL database");
            
            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
