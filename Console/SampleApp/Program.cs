using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = AppStartup();

            var service = ActivatorUtilities.CreateInstance<App>(host.Services);

            service.Run();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            // Check the current directory that the application is running on 
            // Then once the file 'appsetting.json' is found, we are adding it.
            // We add env variables, which can override the configs in appsettings.json
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        static IHost AppStartup()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            BuildConfig(builder);
            IConfigurationRoot configuration = builder.Build();
            // Specifying the configuration for serilog
            Log.Logger = new LoggerConfiguration() // initiate the logger configuration
                            .ReadFrom.Configuration(configuration) // connect serilog to our configuration folder
                            .Enrich.FromLogContext() //Adds more information to our logs from built in Serilog 
                            .CreateLogger(); //initialise the logger

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder() // Initialising the Host 
                        .ConfigureServices((context, services) =>
                        { // Adding the DI container for configuration
                          // 添加 services:
                            services.Configure<AppOption>(configuration.GetSection("AppOption"));
                            services.AddTransient<IDataService, DataService>();
                            services.AddTransient<App>(); // Add transiant mean give me an instance each it is being requested
                        })
                        .UseSerilog() // Add Serilog
                        .Build(); // Build the Host

            return host;
        }
    }
}
