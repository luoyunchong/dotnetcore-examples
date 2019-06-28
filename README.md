# .NET Core  学习示例

> 学习之路漫漫无期

看到java 的spring boot 框架如何火热，examples多达16628 star（2019-6-27），回头看dotnetcore，则不温不火。我想写点示例，降低入门门槛。

- [https://github.com/ityouknow/spring-boot-examples](https://github.com/ityouknow/spring-boot-examples)


假设你已经有了C#基础、ASP .NET MVC或其他语言的MVC基础。 

本项目以C#语言为示例，结合 ASP .NET Core，集成第三方类库的示例，运用基础组件，写好Demo。如果你是一个 .NET Framework开发者，转去学习 .NET Core,你会发现新的世界，我给自己的定位是软件开发工程师，而不是.NET 开发工程师，结合其他牛比的技术才能共赢。


我正在学习和使用的技术、关注的技术
- Linux：Ubuntu
- CLI：PowerShell、Bash
- Docker:Docker for windows、Hyper-v、WSL2
- DevOps:Jenkins、Travis CI、Aurze DevOps
- MySQL、Mariadb
- NoSQL：Redis、MongoDB
- Nginx、
- .NET Core、ASP.NET Core
- RabbitMQ
- SignlaR

关注的开源组织

- dotnetcore :.NET Core Community
    - 官网 https://www.dotnetcore.xyz
    - 开源 https://github.com/dotnetcore
    - 21个开源项目，都是基于dotnetcore开源的优秀项目。
- abpframework：Web Application Framework for ASP .NET Core 
    - 官网 https://abp.io/
    - 开源地址 https://github.com/abpframework
    - abp vnext 完善的基础设施与文档  https://github.com/abpframework/abp

这里向大家推荐阅读

- Microsoft Docs [https://docs.microsoft.com/zh-cn](https://docs.microsoft.com/zh-cn)
    
    其中包含如下
    - .NET Core 指南 https://docs.microsoft.com/zh-cn/dotnet/core/
    - ASP.NET 文档 https://docs.microsoft.com/zh-cn/aspnet/

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


