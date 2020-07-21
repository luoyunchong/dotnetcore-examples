using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace OvOv.Serilog.Filter
{
    public class MvcGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public MvcGlobalExceptionFilter(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("MvcGlobalExceptionFilter");
        }

        public void OnException(ExceptionContext context)
        {
        }
    }
}
