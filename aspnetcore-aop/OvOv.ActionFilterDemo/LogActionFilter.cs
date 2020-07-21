using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace OvOv.ActionFilterDemo
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private readonly ILogger<LogActionFilter> logger;
        Regex regex = new Regex("(?<=\\{)[^}]*(?=\\})");
        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            this.logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LoggerAttribute loggerAttribute = context.ActionDescriptor.EndpointMetadata.OfType<LoggerAttribute>().FirstOrDefault();

            if (loggerAttribute != null)
            {
                string template = loggerAttribute.Template;
                template = this.parseTemplate(template, new UserDO { nickname = "ADMIN" }, context.HttpContext.Request, context.HttpContext.Response);
                logger.LogInformation(template);
            }

            logger.LogInformation("OnActionExecuting---------");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation("OnActionExecuted---------");
        }

        private string parseTemplate(string template, UserDO userDo, HttpRequest request, HttpResponse response)
        {
            foreach (Match item in regex.Matches(template))
            {
                string propertyValue = extractProperty(item.Value, userDo, request, response);
                template = template.Replace("{" + item.Value + "}", propertyValue);
            }

            return template;
        }

        private string extractProperty(string item, UserDO userDo, HttpRequest request, HttpResponse response)
        {
            int i = item.LastIndexOf('.');
            string obj = item.Substring(0, i);
            string prop = item.Substring(i + 1);
            switch (obj)
            {
                case "user":
                    return getValueByPropName(userDo, prop);
                case "request":
                    return getValueByPropName(request, prop);
                case "response":
                    return getValueByPropName(response, prop);
                default:
                    return "";
            }
        }

        private string getValueByPropName<T>(T t, string prop)
        {
            return t.GetType().GetProperty(prop)?.GetValue(t, null)?.ToString();

        }
    }

    public class UserDO
    {
        public string nickname { get; set; }
        public string username { get; set; }
    }
}
