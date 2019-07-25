using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Qiniu.Web
{
    /// <summary>
    /// 过滤API,隐藏无需显示的API
    /// </summary>
    public class HiddenApiFilter : IDocumentFilter
    {
        private readonly IConfigurationRoot _appConfiguration;
        public HiddenApiFilter(IConfigurationRoot config)
        {
            _appConfiguration = config;
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            //如果没有开启API隐藏,则跳过
            if (_appConfiguration["SwaggerDoc:HiddenApi:IsEnabled"] == null) return;
            if (bool.Parse(_appConfiguration["SwaggerDoc:HiddenApi:IsEnabled"]))
            {
                //获取隐藏的API路径(允许逗号分隔)
                var hiddenUrl = _appConfiguration["SwaggerDoc:HiddenApi:HiddenUrls"];
                if (string.IsNullOrWhiteSpace(hiddenUrl))
                {
                    return;
                }
                var hiddenUrls = hiddenUrl.Split(',').ToArray();
                foreach (var group in context.ApiDescriptionsGroups.Items)
                {
                    foreach (var api in group.Items)
                    {
                        var url = api.RelativePath.ToLower();
                        foreach (var item in hiddenUrls)
                        {
                            if (url.Contains(item.ToLower()))
                            {
                                swaggerDoc.Paths.Remove("/" + api.RelativePath);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
