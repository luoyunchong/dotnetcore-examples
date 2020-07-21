using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace OvOv.Serilog.Filter
{
    public class SerilogActionFilter : Attribute, IActionFilter
    {
        private readonly ILogger _logger;
        private  string path;

        public SerilogActionFilter(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("SerilogActionFilter");
        }
    
        public void OnActionExecuted(ActionExecutedContext context)
        {
             path = context.HttpContext.Request.Path;
            _logger.LogDebug("{path} OnActionExecuted",path);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
             path = context.HttpContext.Request.Path;
            _logger.LogDebug("{path} OnActionExecuting",path);
        }
    }
}
