using System;

namespace OvOv.ActionFilterDemo
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LoggerAttribute:Attribute
    {
        public LoggerAttribute(string template)
        {
            Template = template ?? throw new ArgumentNullException(nameof(template));
        }

        public string Template { get; set; }

    }
}
