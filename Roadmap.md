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

Simple .NET logging with fully-structured events. （完全结构化事件的 简单.NET 日志记录组件）

### [NLog](https://github.com/NLog/NLog)

Advanced .NET, Silverlight and Xamarin Logging with support for structured and non structured logging.
（不断发展的.NET、Silverlight 和 Xamarin 日志组件，支持结构化和非结构化日志记录。）

### [log4net](https://github.com/apache/logging-log4net)

## IOC

### [Autofac](https://github.com/autofac/Autofac)

Addictive .NET IoC container.

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

Simple object mapper for .NET

- [Dapper-FluentMap](https://github.com/henkmollema/Dapper-FluentMap)  
  Provides a simple API to fluently map POCO properties to database columns when using Dapper.

- [Dommel](https://github.com/henkmollema/Dommel)
  Simple CRUD operations for Dapper.

- [MicroOrm.Dapper.Repositories](https://github.com/phnx47/MicroOrm.Dapper.Repositories)
  CRUD for Dapper.

### [Chloe](https://github.com/shuxinqin/Chloe)

A lightweight and high-performance Object/Relational Mapping(ORM) library for .NET. 一个轻量级和高性能的对象/关系映射（ORM）库。

### Query Builders

- [SqlKata](https://github.com/sqlkata/querybuilder)
  Elegant Sql Query Builder, that supports complex queries, joins, sub queries, nested where conditions, vendor engine targets and more

### Web Socket

- [SignalR](https://github.com/dotnet/aspnetcore/blob/master/src/SignalR/README.md)

ASP.NET Core SignalR 是消息推送组件库，兼容性强，有降级处理。
