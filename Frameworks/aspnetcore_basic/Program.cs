using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<BasicAuthenticationOption>(builder.Configuration.GetSection("Basic"));

BasicAuthenticationOption basicOption = new BasicAuthenticationOption();
builder.Configuration.Bind("Basic", basicOption);

//https://www.cnblogs.com/JulianHuang/p/10345365.html
builder.Services.AddAuthentication(BasicAuthenticationScheme.DefaultScheme)
                .AddScheme<BasicAuthenticationOption, BasicAuthenticationHandler>(BasicAuthenticationScheme.DefaultScheme, r =>
                {
                    r.UserName = basicOption.UserName;
                    r.UserPwd = basicOption.UserPwd;
                    r.Realm = basicOption.Realm;
                });

var app = builder.Build();


/// <summary>
/// _protectedResourceOption.Path
/// </summary>
app.UseWhen(
            predicate: x => x.Request.Path.StartsWithSegments(new PathString("/swagger/index.html")) 
            || x.Request.Path.StartsWithSegments(new PathString("/swagger/v1/swagger.json"))
            ,
            configuration: appBuilder => { appBuilder.UseBasicAuthentication(); }
    );

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
