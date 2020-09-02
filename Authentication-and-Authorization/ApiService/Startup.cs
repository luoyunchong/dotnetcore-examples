using System;
using System.Collections.Generic;
using System.IO;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ApiService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // IdentityServer
            services.AddAuthentication(Configuration["Identity:Scheme"])
                .AddIdentityServerAuthentication(options =>
                {
                    options.RequireHttpsMetadata = false; // for dev env
                    options.Authority = $"http://{Configuration["Identity:IP"]}:{Configuration["Identity:Port"]}";
                    options.ApiName = Configuration["Service:Name"]; // match with configuration in IdentityServer
                });

            #region Swagger

            //Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo() { Title = "ApiService", Version = "v1" });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, Array.Empty<string>() }
                });//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "oauth2",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, Array.Empty<string>() }
                });
                // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri("http://localhost:5001/connect/authorize", UriKind.Absolute),
                            TokenUrl = new Uri("http://localhost:5001/connect/token", UriKind.Absolute),
                            Scopes = new Dictionary<string, string>
                            {
                                { "readAccess", "Access read openid" },
                                { "writeAccess", "Access read email" },
                            }
                        },
                        Password = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri("http://localhost:5001/connect/authorize", UriKind.Absolute),
                            TokenUrl = new Uri("http://localhost:5001/connect/token", UriKind.Absolute),
                            Scopes = new Dictionary<string, string>
                            {
                                { "readAccess", "Access read openid" },
                                { "writeAccess", "Access read email" },
                            }
                        }
                    }
                });

                options.AddServer(new OpenApiServer()
                {
                    Url = "/",
                    Description = "本地"
                });
                options.AddServer(new OpenApiServer()
                {
                    Url = "https://api.igeekfan.cn/",
                    Description = "服务器"
                });
                options.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return $"ID-{controllerAction.GetHashCode()}";
                });

                string xmlPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");
                options.IncludeXmlComments(xmlPath, true);

            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                c.EnableDeepLinking();
                c.OAuthClientId("client.api.service");
                c.OAuthClientSecret("clientsecret");
                //c.OAuthUsePkce();
                c.OAuthScopeSeparator(" ");
            });

            app.UseKnife4UI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                c.RoutePrefix = "";
                c.OAuthClientId("client.api.service");
                c.OAuthClientSecret("clientsecret");
                c.OAuthUsePkce();
                c.OAuthScopeSeparator(" ");
            });

            app.UseAuthentication();
            app.UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
