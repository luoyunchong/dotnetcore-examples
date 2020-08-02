using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using Serilog.Sinks.SystemConsole.Themes;

namespace OvOv.Serilog.ConsoleDemo
{
    public static class Program
    {
        public static void Main()
        {
            //GettingStart();

            FromAppSettings_JSON();

            //FromAppConfig();


            Log.Information("Hello {Name} from thread {ThreadId}", Environment.GetEnvironmentVariable("USERNAME"), Thread.CurrentThread.ManagedThreadId);

            Log.Warning("No coins remain at position {@Position}", new { Lat = 25, Long = 134 });

            Log.CloseAndFlush();
        }

        static void GettingStart()
        {
            Log.Logger = new LoggerConfiguration()
                //.WriteTo.Console()
                .WriteTo.Console(
                    theme: SystemConsoleTheme.Literate,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            Log.Information("Hello, world!");
        }


        public static void FromAppConfig()
        {
            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.AppSettings()
                        .CreateLogger();
        }


        public static void FromAppSettings_JSON()
        {
            var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}