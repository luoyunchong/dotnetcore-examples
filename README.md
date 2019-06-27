# .NET Core  学习示例

看到java 的spring boot 框架如何火热，examples多达16628 star（2019-6-27），回头看dotnetcore，则不温不火。我想写点示例，降低入门门槛。

- [https://github.com/ityouknow/spring-boot-examples](https://github.com/ityouknow/spring-boot-examples)


假设你已经有了C#基础、ASP .NET MVC或其他语言的MVC基础。 

本项目以C#语言为示例，结合 ASP .NET Core，集成第三方类库的示例，运用基础组件，写好Demo。如果你是一个 .NET Framework开发者，转去学习 .NET Core,你会发现新的世界，因为我们不能只固化在一种语言中，结合基于技术才能共赢。


我正在学习和使用的技术、关注的技术
- Linux：Ubuntu
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

https://dotnet.microsoft.com/download/dotnet-core/3.0

in PowerShell 中任一目录查看安装后的版本 

~~~PowerShell
PS D:\code\github\dotnetcore-examples> dotnet --version
3.0.100-preview6-012264
~~~

## CLI
全称：command-line interface，命令行界面，主要是cmd、bash(sh等等)、powershell等。
创建一个hello-word的console，会输出Hello World!

~~~bash
mkdir console-hello-world
cd console-hello-world
dotnet new console
dotnet run
~~~

