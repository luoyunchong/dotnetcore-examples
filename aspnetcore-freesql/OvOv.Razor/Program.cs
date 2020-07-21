using System;
using System.IO;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace Helpers
{
    public static class TextHelper
    {
        public static string Decorate(string value)
        {
            return "-= " + value + " =-";
        }
    }
}

namespace OvOv.Razor
{

    class Program
    {
        static void Main(string[] args)
        {
            string template = "Hello @Model.Name, welcome to RazorEngine!";
            var result = Engine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });

            Console.WriteLine(result);

            string templateFilePath = "HelloWorld.cshtml";
            var templateFile = File.ReadAllText(templateFilePath);
            string templateFileResult = Engine.Razor.RunCompile(templateFile, Guid.NewGuid().ToString(), null, new
            {
                Name = "World"
            });

            Console.WriteLine(templateFileResult);

            string copyRightTemplatePath = "CopyRightTemplate.cshtml";
            var copyRightTemplate = File.ReadAllText(copyRightTemplatePath);
            string copyRightResult = Engine.Razor.RunCompile(copyRightTemplate, Guid.NewGuid().ToString(), typeof(CopyRightUserInfo), new CopyRightUserInfo
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                UserName = "IGeekFan"
            });
            Console.WriteLine(copyRightResult);

            ITemplateServiceConfiguration configuration = new TemplateServiceConfiguration()
            {
                Language=Language.CSharp,
                EncodedStringFactory=new RawStringFactory(),
                Debug=true
            };
            configuration.Namespaces.Add("Helpers");

            IRazorEngineService service = RazorEngineService.Create(configuration);

            string template2 = @"Hello @Model.Name, @TextHelper.Decorate(Model.Name)";
            string result2 = service.RunCompile(template2, "templateKey", null, new { Name = "World" });
            Console.WriteLine(result2);

            Engine.Razor = service;

            string template3 = "Hello @Model.Name, welcome to RazorEngine!";
            string templateFile3 = "C:/mytemplate.cshtml";
            var result3 =
                Engine.Razor.RunCompile(new LoadedTemplateSource(template3, templateFile3), "templateKey3", null, new { Name = "World" });
            Console.WriteLine(result3);

            Console.ReadKey();

        }
    }
}
