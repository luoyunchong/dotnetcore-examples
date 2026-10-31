[English](Roadmap.en.md) | **简体中文**

<!-- TOC -->

- [Logging](#logging)
  - [serilog](#serilog)
  - [NLog](#nlog)
  - [log4net](#log4net)
- [IOC](#ioc)
  - [Autofac](#autofac)
- [Caching](#caching)
  - [csredis](#csredis)
  - [Stack Exchange Redis](#stack-exchange-redis)
- [ORM](#orm)
  - [Freesql](#freesql)
  - [Entity Framework Core](#entity-framework-core)
  - [StackExchange/Dapper](#stackexchangedapper)
  - [Chloe](#chloe)
  - [Query Builders](#query-builders)
  - [Web Socket](#web-socket)

<!-- /TOC -->

## Logging

### [serilog](https://github.com/serilog/serilog)

支持完全结构化事件的简洁 .NET 日志组件。

### [NLog](https://github.com/NLog/NLog)

支持 .NET、Silverlight 和 Xamarin 的高级日志组件，兼容结构化和非结构化日志记录。

### [log4net](https://github.com/apache/logging-log4net)

## IOC

### [Autofac](https://github.com/autofac/Autofac)

易用的 .NET IoC 容器。

## Caching

### [csredis](https://github.com/2881099/csredis)

支持 redis、高性能、分区、集群、哨兵

- [demo](https://github.com/luoyunchong/dotnetcore-examples/blob/master/dotnetcore-redis/dotnetcore-redis.sln)
- [csredis-in-asp.net core 理论实战-主从配置、哨兵模式](https://blog.igeekfan.cn/2019/07/06/re-start/csredis-in-asp-net-core-master-slaver/)
- [csredis-in-asp.net-core 理论实战-使用示例](https://blog.igeekfan.cn/2019/07/07/re-start/csredis-in-asp.net-core-how-to-use/)

### [Stack Exchange Redis](https://github.com/StackExchange/StackExchange.Redis)

用于.NET 语言的高性能通用 redis 客户端(C#等)，redis 驱动、良好的文档、stackoverflow 出品

- 官方文档 : [https://stackexchange.github.io/StackExchange.Redis/](https://stackexchange.github.io/StackExchange.Redis/)
- [demo](https://github.com/luoyunchong/dotnetcore-examples/blob/master/dotnetcore-redis/dotnetcore-redis.sln)

## ORM

### [Freesql](https://github.com/2881099/FreeSql)

FreeSql 是功能强大的对象关系映射技术(O/RM)，支持 .NETCore 2.1+ 或 .NETFramework 4.0+ 或 Xamarin。支持 CodeFirst、DbFirst ，多种数据库，读写分离、分表分库、过滤器、乐观锁、悲观锁；

- 官方文档 : [https://github.com/dotnetcore/FreeSql/wiki](https://github.com/dotnetcore/FreeSql/wiki)

- [FreeSql 在 ASP.NTE Core WebApi 中如何使用的教程](https://blog.igeekfan.cn/2019/06/30/re-start/FreeSql-aspnetcore-how-to-use/)
- [使用 RESTful、FreeSql 构建简单的博客系统-集成 AutoMapper](https://blog.igeekfan.cn/2019/06/30/re-start/FreeSql-sample-blog-RESTful/)

### [Entity Framework Core](https://github.com/dotnet/efcore)

Entity Framework (EF) Core 是轻量化、可扩展、开源和跨平台版的常用 Entity Framework 数据访问技术。
EF Core 可用作对象关系映射程序 (O/RM)，以便于 .NET 开发人员能够使用 .NET 对象来处理数据库，这样就不必经常编写大部分数据访问代码了。

- 官方文档 : [https://docs.microsoft.com/zh-cn/ef/core/](https://docs.microsoft.com/zh-cn/ef/core/)

### [StackExchange/Dapper](https://github.com/StackExchange/Dapper)

适用于 .NET 的简洁对象映射器

- [Dapper-FluentMap](https://github.com/henkmollema/Dapper-FluentMap)  
  提供简洁 API，在使用 Dapper 时以流式方式将 POCO 属性映射到数据库列。

- [Dommel](https://github.com/henkmollema/Dommel)
  为 Dapper 提供简洁的增删改查操作。

- [MicroOrm.Dapper.Repositories](https://github.com/phnx47/MicroOrm.Dapper.Repositories)
  为 Dapper 提供增删改查操作。

### [Chloe](https://github.com/shuxinqin/Chloe)

适用于 .NET 的轻量级、高性能对象关系映射（ORM）库。

### Query Builders

- [SqlKata](https://github.com/sqlkata/querybuilder)
  SQL 查询构建器，支持复杂查询、连接、子查询、嵌套条件和多种数据库引擎。

### Web Socket

- [SignalR](https://github.com/dotnet/aspnetcore/blob/master/src/SignalR/README.md)

ASP.NET Core SignalR 是消息推送组件库，兼容性强，有降级处理。
