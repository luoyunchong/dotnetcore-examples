# .NET Core  学习示例文档

浏览左侧导航菜单以深入了解文档.

.NET Core 学习示例文档主要是结合 ASP .NET Core，集成第三方类库的示例，运用基础组件，写好Demo。

## 源码
代码托管在GitHub上  [https://github.com/luoyunchong/dotnetcore-examples](https://github.com/luoyunchong/dotnetcore-examples)

## 推荐阅读

- Microsoft Docs [https://docs.microsoft.com/zh-cn](https://docs.microsoft.com/zh-cn)
    
    其中包含如下
    - .NET Core 指南 https://docs.microsoft.com/zh-cn/dotnet/core/
    - ASP .NET 文档 https://docs.microsoft.com/zh-cn/aspnet/
    - .NET Core CLI 文档 https://docs.microsoft.com/zh-cn/dotnet/core/tools/?tabs=netcore2x

## 本地环境说明
- Windows 10 (18922.rs_prerelease.190614-1427)
- .NET Core 3.0.100-preview6-012264
- Visual Studio Code 1.35.1、Microsoft Visual Studio 2019 16.1.3
- PowerSheel 
- MySQL 5.7.25
- Navicat Premium 12 [欢迎下载](http://blog.igeekfan.cn/2018/06/02/%E5%A4%A7%E5%90%8E%E7%AB%AF/Navicat%20Premium%2012%20%20%E7%A0%B4%E8%A7%A3%E7%89%88%E5%85%8D%E8%B4%B9%E4%B8%8B%E8%BD%BD/)
## Install

本地开发选择SDK安装即可，还是安装 2.2的吧，3.0（19.6.29）目前还没有发布稳定版本。

- 安装这个 **https://dotnet.microsoft.com/download/dotnet-core/2.2**
- https://dotnet.microsoft.com/download/dotnet-core/3.0

安装后，在 PowerShell 中任一目录查看安装后的版本 

~~~PowerShell
PS C:\WINDOWS\system32> dotnet --version
3.0.100-preview6-012264
# 本地安装了好几个.net core sdk版本
PS C:\WINDOWS\system32> dotnet --list-sdks
2.1.700 [C:\Program Files\dotnet\sdk]
2.2.300 [C:\Program Files\dotnet\sdk]
3.0.100-preview6-012264 [C:\Program Files\dotnet\sdk]
~~~

## CLI
全称：command-line interface，命令行界面，主要是cmd、bash(sh等等)、powershell等。

> 说明 **所有命令行都在windows10自带的powershell中执行。** 


### 指定SDK版本
 .NET Core 项目默认使用最新版本的 .NET Core，在根目录使用PowerShell中执行如下命令，

 语法 ：dotnet new global.json --sdk-version <SDK版本号>
~~~
dotnet new globaljson --sdk-version 2.2.300
~~~
参考
- [dotnet new 命令行](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22)
- [选择要使用的 .NET Core 版本](https://docs.microsoft.com/zh-cn/dotnet/core/versions/selection?view=dotnet-plat-ext-2.1)
## .NET Core

.NET Core是什么？ 官网说的太高大上了 https://docs.microsoft.com/zh-cn/dotnet/core/about
 
总结，.NET Core包含如下
- 二个RunTime
    - .NET Core RunTime：基础类型系统 、垃圾回收、基元类型等，
    - ASP .NET RunTime ：提供WEB、LOT等应用程序的框架支持。
- .NET Core CLI工具：各种命令行工具，创建项目、编译项目，发布项目等；
- 语言编译器：（支持C#、F#、VB等语言）
- dotnet 工具：.NET Core运行时和库的安装程序包

三个发布包：
- .NET Core 运行时 
-  ASP .NET Core 运行时 
- .NET Core SDK：包括上面二个内容，再加上 .NET CLI工具等

所以本地调试时，可直接安装最全的SDK即可。

## 项目文档

目前 关于此项目的文档放到docs文件夹中，zh-Hans为中文，这样可支持多语言，欢迎翻译PR，之后会发布至 

-  文档官网 [http://docs.igeekfan.cn](http://docs.igeekfan.cn)

将使用abp vnext 下的modules的[docs模块](https://github.com/abpframework/abp/blob/dev/modules/docs/README.md)。不过abp vnext 现在也不稳定，0.18.1，还是有各种问题，我还是写基础模块的使用文档吧，后期完善后，发布文档网站。


## 说明
本项目也是我的学习记录，，用于测试不同类库集成的解决方案，所以用最基础的方案，**命令行**来创建项目，引用包，运行，测试等。让自己对 .net core 的原理结构了解地更加深入一些。
- 采用的都是 Visual Studio Code +PowerShell运行，关于如何采用Visual Studio 2019创建项目，引用包是非常简单的，不再说明。
- 