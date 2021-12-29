using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nacos.V2.DependencyInjection;
using Nacos.V2.Naming.Dtos;
using NacosConsole;

var host = AppStartup();

var service = ActivatorUtilities.CreateInstance<App>(host.Services);
await service.RunAsync(args);


//var app = host.Services.GetService<App>();
//await app.RunAsync(args);

static IHost AppStartup()
{
    var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context, services);
                    services.AddTransient<App>();
                })
                .ConfigureAppConfiguration((host, config) =>
                {

                })
                .Build(); // Build the Host

    return host;
}


static void ConfigureServices(HostBuilderContext context,IServiceCollection services)
{
    services.AddNacosV2Config(x =>
    {
        x.ServerAddresses = new System.Collections.Generic.List<string> { "http://localhost:8848/" };
        x.EndPoint = "";
        x.Namespace = "cs-test";

        /*x.UserName = "nacos";
       x.Password = "nacos";*/

        // swich to use http or rpc
        x.ConfigUseRpc = true;
    });

    services.AddNacosV2Naming(x =>
    {
        x.ServerAddresses = new System.Collections.Generic.List<string> { "http://localhost:8848/" };
        x.EndPoint = "";
        x.Namespace = "cs-test";

        /*x.UserName = "nacos";
       x.Password = "nacos";*/

        // swich to use http or rpc
        x.NamingUseRpc = true;
    });
}
