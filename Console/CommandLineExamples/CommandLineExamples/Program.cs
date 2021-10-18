using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace CommandLineExamples
{
    class Program
    {
        /// <summary>
        ///  dotnet run -- -h
        ///  dotnet run -- --version
        ///  dotnet run -- example -t 标题
        ///  dotnet run -- example --title 标题
        /// </summary>
        static async Task<int> Main(string[] args)
        {
            var parser = BuildCommandLine()
             .UseHost(_ => Host.CreateDefaultBuilder(args), (builder) =>
             {
                 builder.ConfigureServices((hostContext, services) =>
                 {
                     IConfiguration configuration = hostContext.Configuration;
                     // register other dependencies here
                 })
                 .UseCommandHandler<ExampleCommand, ExampleCommand.Handler>();
             }).UseDefaults().Build();
            return await parser.InvokeAsync(args);

        }
        static CommandLineBuilder BuildCommandLine()
        {
            var root = new RootCommand();
            root.AddCommand(new ExampleCommand());
            return new CommandLineBuilder(root);
        }
    }

}
