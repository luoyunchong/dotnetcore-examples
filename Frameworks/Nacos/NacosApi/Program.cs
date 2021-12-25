using NacosApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<UserInfo>(builder.Configuration.GetSection("UserInfo"));

builder.Host.ConfigureAppConfiguration((context, builder) =>
        {
            var c = builder.Build();

            // 从配置文件读取Nacos相关配置
            // 默认会使用JSON解析器来解析存在Nacos Server的配置
            builder.AddNacosV2Configuration(c.GetSection("NacosConfig"));
            // 也可以按需使用ini或yaml的解析器
            // builder.AddNacosV2Configuration(c.GetSection("NacosConfig"), Nacos.IniParser.IniConfigurationStringParser.Instance);
            // builder.AddNacosV2Configuration(c.GetSection("NacosConfig"), Nacos.YamlParser.YamlConfigurationStringParser.Instance);
        });

var d=builder.Configuration["commonkey"];


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
