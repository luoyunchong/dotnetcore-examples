# nacos-csharp-demo

### nacos

nacos 是一个构建云原生应用的动态服务发现、配置管理和服务管理平台。。

在这里，我们通过 csharp配合nacos实现对我们的配置进行管理。

- nacos github https://github.com/alibaba/nacos
- csharp sdk github https://github.com/nacos-group/nacos-sdk-csharp
- csharp sdk 文档：https://nacos-sdk-csharp.readthedocs.io/en/latest/introduction/gettingstarted.html
- https://nacos.io/zh-cn/

安装请参考：https://nacos.io/zh-cn/docs/quick-start.html

### 开始

- windows 进入nacos的bin目录
```
startup.cmd -m standalone
```

默认运行在8848端口
- http://localhost:8848/nacos/#/login
- nacos
- nacos

## 必做
登录后，打开**命名空间**->新建命名空间->
- ` 命名空间ID`:这里填，`cs-test`，注意下方的配置项Namespace请填写此值。
- `命名空间名：`这个只是用于展示区分，填`cs-test`，建议直接和命名空间id相同即可。
- `描述：`:这个随便填


## Nacos+Console
新建一个控制台项目

引入包
```
<PackageReference Include="Microsoft.Extensions.Hosting" Version="6.0.0" />
<PackageReference Include="nacos-sdk-csharp" Version="1.2.2" />
```

```csharp
static IHost AppStartup()
{
    var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context, services);
                    services.AddTransient<App>();
                })
                .ConfigureAppConfiguration((host, config) =>
                {

                })
                .Build(); // Build the Host

    return host;
}

```

配置nacos的服务
```csharp
static void ConfigureServices(HostBuilderContext context,IServiceCollection services)
{
    services.AddNacosV2Config(x =>
    {
        x.ServerAddresses = new System.Collections.Generic.List<string> { "http://localhost:8848/" };
        x.EndPoint = "";
        x.Namespace = "cs-test";

        /*x.UserName = "nacos";
       x.Password = "nacos";*/

        // swich to use http or rpc
        x.ConfigUseRpc = true;
    });

    services.AddNacosV2Naming(x =>
    {
        x.ServerAddresses = new System.Collections.Generic.List<string> { "http://localhost:8848/" };
        x.EndPoint = "";
        x.Namespace = "cs-test";

        /*x.UserName = "nacos";
       x.Password = "nacos";*/

        // swich to use http or rpc
        x.NamingUseRpc = true;
    });
}
```

调用 
```csharp
var host = AppStartup();
var service = ActivatorUtilities.CreateInstance<App>(host.Services);
await service.RunAsync(args);
```
App.cs文件配置

```csharp
public class App
{
    private readonly ILogger<App> _logger;
    private readonly INacosConfigService _ns;
    public App(ILogger<App> logger, INacosConfigService ns)
    {
        _logger = logger;
        _ns = ns;
    }

    public async Task RunAsync(string[] args)
    {
        await PublishConfig(_ns);
        await GetConfig(_ns);
        await RemoveConfig(_ns);
    }

    static async Task PublishConfig(INacosConfigService svc)
    {
        var dataId = "demo-dateid";
        var group = "demo-group";
        var val = "test-value-" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

        await Task.Delay(500);
        var flag = await svc.PublishConfig(dataId, group, val);
        Console.WriteLine($"======================发布配置结果，{flag}");
    }

    static async Task GetConfig(INacosConfigService svc)
    {
        var dataId = "demo-dateid";
        var group = "demo-group";

        await Task.Delay(500);
        var config = await svc.GetConfig(dataId, group, 5000L);
        Console.WriteLine($"======================获取配置结果，{config}");
    }

    static async Task RemoveConfig(INacosConfigService svc)
    {
        var dataId = "demo-dateid";
        var group = "demo-group";

        await Task.Delay(500);
        var flag = await svc.RemoveConfig(dataId, group);
        Console.WriteLine($"=====================删除配置结果，{flag}");
    }
}

```

f5运行后可看到输出如下内容 

```
======================发布配置结果，True
======================获取配置结果，test-value-1637000754
=====================删除配置结果，True
```

我们把`await RemoveConfig(_ns);`这行删除，即可在nacos的网站上看到信息。

配置管理 -选`cs-test`,可以看到`Data Id为demo-dateid`，`Group`为`demo-group`的一行数据，点击行内的编辑即可看到具体信息。

## 博客
- [聊一聊如何在.NET Core中使用Nacos 2.0](https://mp.weixin.qq.com/s/iC6lFJJsHUFUveSJhoZxgA)
