using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace CommandLineExamples
{
    public class ExampleCommand : Command
    {
        public ExampleCommand() : base(name: "example", "Example description")
        {

            this.AddOption(new Option<string>(
                new string[] { "--title", "-t" }, "Title of the Example description"));
        }


        public new class Handler : ICommandHandler
        {
            public string Title { get; set; }
            public Task<int> InvokeAsync(InvocationContext context)
            {
                Console.WriteLine(Title);
                return Task.FromResult(0);
            }
        }
    }

}
