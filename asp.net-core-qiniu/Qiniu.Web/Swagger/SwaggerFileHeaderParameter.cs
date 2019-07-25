using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Qiniu.Web.Swagger
{
    /// <summary>
    /// swagger文件过滤器
    /// </summary>
    public class SwaggerFileHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<IParameter>();

            if (!context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) &&
                !context.ApiDescription.HttpMethod.Equals("PUT", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }


            var fileParameters = context.ApiDescription.ParameterDescriptions.Where(n => n.Type == typeof(IFormFileCollection) || n.Type == typeof(IFormFile)).ToList();
            //parameterDescriptions包含了每个接口所带所有参数信息

            if (fileParameters.Count <= 0)
            {
                return;
            }

            operation.Consumes.Add("multipart/form-data");

            foreach (var fileParameter in fileParameters)
            {
                var parameter = operation.Parameters.Single(n => n.Name == fileParameter.Name);
                operation.Parameters.Remove(parameter);
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = parameter.Name,
                    In = "formData",
                    Description = parameter.Description,
                    Required = parameter.Required,
                    Type = "file",
                    //CollectionFormat = "multi"
                });
            }
        }
    }
}
