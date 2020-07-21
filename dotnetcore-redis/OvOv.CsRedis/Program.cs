using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OvOv.CsRedis;

namespace OvOv.CsRedis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
