using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace OvOv.Nlog.Filter
{
    public class NlogActionFilter : Attribute, IActionFilter
    {
        private readonly ILogger<NlogActionFilter> logger;
        public NlogActionFilter(ILogger<NlogActionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Query.TryGetValue("id", out StringValues value))
            {
                Console.WriteLine(value.First());
                logger.LogInformation(value.First());
            }
            else
            {
                Console.WriteLine("没有获取id");
                logger.LogError("没有获取id");
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
