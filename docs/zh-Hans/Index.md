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

## 本地环境说明
- Windows 10 (18922.rs_prerelease.190614-1427)
- .NET Core 3.0.100-preview6-012264
- Visual Studio Code 1.35.1
- PowerSheel 7-Preview.1

## Install

本地开发选择SDK安装即可。

https://dotnet.microsoft.com/download/dotnet-core/3.0

安装后，在 PowerShell 中任一目录查看安装后的版本 

~~~PowerShell
PS D:\code\github\dotnetcore-examples> dotnet --version
3.0.100-preview6-012264
~~~

## CLI
全称：command-line interface，命令行界面，主要是cmd、bash(sh等等)、powershell等。


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



