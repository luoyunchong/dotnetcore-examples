using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Qiniu.Web.Swagger
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// 添加自定义API文档生成(支持文档配置)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        public static void AddCustomSwaggerGen(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            if (configuration?["SwaggerDoc:IsEnabled"] == null)
            {
                return;
            }
            if (bool.Parse(configuration["SwaggerDoc:IsEnabled"]))
            {
                var webRootDirectory = hostingEnvironment.WebRootPath ?? Directory.GetCurrentDirectory();
                //设置API文档生成
                services.AddSwaggerGen(options =>
                {
                    //将所有枚举显示为字符串
                    if (bool.Parse(configuration["SwaggerDoc:DescribeAllEnumsAsStrings"]))
                        options.DescribeAllEnumsAsStrings();

                    options.SwaggerDoc(configuration["SwaggerDoc:Name"], new Info
                    {
                        Title = configuration["SwaggerDoc:Title"],
                        Version = configuration["SwaggerDoc:Version"],
                        Description = configuration["SwaggerDoc:Description"],
                        Contact = new Contact
                        {
                            Name = configuration["SwaggerDoc:Contact:Name"],
                            Email = configuration["SwaggerDoc:Contact:Email"]
                        }
                    });

                    if (bool.Parse(configuration["SwaggerDoc:Authorize"] ?? "false"))
                    {
                        //以便于在界面上显示验证（Authorize）按钮，验证按钮处理逻辑基于 wwwroot/swagger/ui/index.html
                        options.AddSecurityDefinition("Bearer", new BasicAuthScheme());
                    }


                    {

                        //var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                        //options.AddSecurityRequirement(security);//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                        //options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                        //{
                        //    Description = "权限认证(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                        //    Name = "Authorization",//jwt默认的参数名称
                        //    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                        //    Type = "apiKey"
                        //});//Authorization的设置
                    }


                    //遍历所有xml并加载
                    var paths = new List<string>();
                    if (!string.IsNullOrWhiteSpace(webRootDirectory) && Directory.Exists(webRootDirectory))
                    {
                        var plusPath = Path.Combine(webRootDirectory, "PlugIns");
                        if (Directory.Exists(plusPath))
                        {
                            var xmlFiles = new DirectoryInfo(plusPath).GetFiles("*.xml");
                            foreach (var item in xmlFiles)
                            {
                                paths.Add(item.FullName);
                            }
                        }
                    }

                    var binXmlFiles = new DirectoryInfo(hostingEnvironment.ContentRootPath).GetFiles("*.xml", hostingEnvironment.EnvironmentName == "Development" 
                        ? SearchOption.AllDirectories 
                        : SearchOption.TopDirectoryOnly);

                    foreach (var item in binXmlFiles)
                    {
                        paths.Add(item.FullName);
                    }

                    foreach (var filePath in paths)
                    {
                        options.IncludeXmlComments(filePath);
                    }
                    options.DocInclusionPredicate((docName, description) => true);
                    options.DocumentFilter<HiddenApiFilter>(configuration);

                    if (configuration["SwaggerDoc:UseFullNameForSchemaId"] != null && bool.Parse(configuration["SwaggerDoc:UseFullNameForSchemaId"]))
                    {
                        //使用全名作为架构id
                        options.CustomSchemaIds(p => p.FullName);
                    }

                    options.OperationFilter<SwaggerFileHeaderParameter>();//增加文件过滤处理
                });
            }
        }

        /// <summary>
        /// 启用自定义API文档(支持文档配置)
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void UseCustomSwaggerUI(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (configuration?["SwaggerDoc:IsEnabled"] == null)
            {
                return;
            }
            if (bool.Parse(configuration["SwaggerDoc:IsEnabled"]))
            {
                app.UseSwagger(c => { c.RouteTemplate = "{documentName}/swagger.json"; });
                // 加载swagger-ui 资源 (HTML, JS, CSS etc.)
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"/{configuration["SwaggerDoc:Name"]}/swagger.json", configuration["SwaggerDoc:Title"] ?? "App API V1");
                    //允许通过嵌入式资源配置首页
                    if (!string.IsNullOrWhiteSpace(configuration["SwaggerDoc:ManifestResourceUrl"]))
                    {
                        options.IndexStream = () =>
                        Assembly.Load(configuration["SwaggerDoc:ManifestResourceAssembly"])
                   .GetManifestResourceStream(configuration["SwaggerDoc:ManifestResourceUrl"]);
                    }
                });
            }
        }
    }
}
