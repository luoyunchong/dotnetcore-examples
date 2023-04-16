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
        private readonly ILogger<LogActionFilter> _logger;
        private readonly Regex _regex = new Regex("(?<=\\{)[^}]*(?=\\})");
        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LoggerAttribute? loggerAttribute = context.ActionDescriptor.EndpointMetadata.OfType<LoggerAttribute>().FirstOrDefault();

            if (loggerAttribute != null)
            {
                string template = loggerAttribute.Template;
                template = this.ParseTemplate(template, new UserDO { Nickname = "系统管理员", Username = "ADMIN" }, context.HttpContext.Request, context.HttpContext.Response);
                _logger.LogInformation(template);
            }

            _logger.LogInformation("OnActionExecuting---------");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("OnActionExecuted---------");
        }

        private string ParseTemplate(string template, UserDO userDo, HttpRequest request, HttpResponse response)
        {
            foreach (Match item in _regex.Matches(template))
            {
                string? propertyValue = ExtractProperty(item.Value, userDo, request, response);
                template = template.Replace("{" + item.Value + "}", propertyValue);
            }

            return template;
        }

        private string? ExtractProperty(string item, UserDO userDo, HttpRequest request, HttpResponse response)
        {
            int i = item.LastIndexOf('.');
            string obj = item.Substring(0, i);
            string prop = item.Substring(i + 1);
            switch (obj)
            {
                case "user":
                    return GetValueByPropName(userDo, prop);
                case "request":
                    return GetValueByPropName(request, prop);
                case "response":
                    return GetValueByPropName(response, prop);
                default:
                    return "";
            }
        }

        private static string? GetValueByPropName<T>(T t, string prop)
        {
            return t.GetType().GetProperty(prop)?.GetValue(t, null)?.ToString();

        }
    }

    /// <summary>
    /// 当前登录实体
    /// </summary>
    public class UserDO
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string Username { get; set; }
    }
}
