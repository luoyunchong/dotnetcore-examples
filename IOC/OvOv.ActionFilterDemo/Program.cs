
using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OvOv.ActionFilterDemo;

var builder = WebApplication.CreateBuilder(args);
var c = builder.Configuration;

builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogActionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "OvOv.ActionFilterDemo.xml"), true));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();