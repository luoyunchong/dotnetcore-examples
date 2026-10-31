**English** | [简体中文](Roadmap.zh-CN.md)

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

Simple .NET logging with fully-structured events. 

### [NLog](https://github.com/NLog/NLog)

Advanced .NET, Silverlight and Xamarin Logging with support for structured and non structured logging.


### [log4net](https://github.com/apache/logging-log4net)

## IOC

### [Autofac](https://github.com/autofac/Autofac)

Addictive .NET IoC container.

## Caching

### [csredis](https://github.com/2881099/csredis)

A high-performance Redis client supporting partitioning, clusters, and Sentinel.

- [demo](https://github.com/luoyunchong/dotnetcore-examples/blob/master/dotnetcore-redis/dotnetcore-redis.sln)
- [csredis with ASP.NET Core: primary-replica configuration and Sentinel](https://blog.igeekfan.cn/2019/07/06/re-start/csredis-in-asp-net-core-master-slaver/)
- [csredis with ASP.NET Core: usage examples](https://blog.igeekfan.cn/2019/07/07/re-start/csredis-in-asp.net-core-how-to-use/)

### [Stack Exchange Redis](https://github.com/StackExchange/StackExchange.Redis)

A high-performance general-purpose Redis client for .NET, with a Redis driver and documentation, from Stack Overflow.

- Official documentation : [https://stackexchange.github.io/StackExchange.Redis/](https://stackexchange.github.io/StackExchange.Redis/)
- [demo](https://github.com/luoyunchong/dotnetcore-examples/blob/master/dotnetcore-redis/dotnetcore-redis.sln)

## ORM

### [Freesql](https://github.com/2881099/FreeSql)

FreeSql is an object-relational mapper for .NET Core 2.1+, .NET Framework 4.0+, and Xamarin. It supports Code First, Database First, multiple databases, read/write separation, sharding, filters, and optimistic and pessimistic locking.

- Official documentation : [https://github.com/dotnetcore/FreeSql/wiki](https://github.com/dotnetcore/FreeSql/wiki)

- [How to use FreeSql in ASP.NET Core Web API](https://blog.igeekfan.cn/2019/06/30/re-start/FreeSql-aspnetcore-how-to-use/)
- [Build a simple RESTful blog with FreeSql and AutoMapper](https://blog.igeekfan.cn/2019/06/30/re-start/FreeSql-sample-blog-RESTful/)

### [Entity Framework Core](https://github.com/dotnet/efcore)

Entity Framework (EF) Core is a lightweight, extensible, open-source, cross-platform version of Entity Framework.
EF Core is an object-relational mapper that lets .NET developers work with databases using .NET objects, reducing the need for much of the usual data-access code.

- Official documentation : [https://docs.microsoft.com/zh-cn/ef/core/](https://docs.microsoft.com/zh-cn/ef/core/)

### [StackExchange/Dapper](https://github.com/StackExchange/Dapper)

Simple object mapper for .NET

- [Dapper-FluentMap](https://github.com/henkmollema/Dapper-FluentMap)  
  Provides a simple API to fluently map POCO properties to database columns when using Dapper.

- [Dommel](https://github.com/henkmollema/Dommel)
  Simple CRUD operations for Dapper.

- [MicroOrm.Dapper.Repositories](https://github.com/phnx47/MicroOrm.Dapper.Repositories)
  CRUD for Dapper.

### [Chloe](https://github.com/shuxinqin/Chloe)

A lightweight and high-performance Object/Relational Mapping(ORM) library for .NET.

### Query Builders

- [SqlKata](https://github.com/sqlkata/querybuilder)
  Elegant Sql Query Builder, that supports complex queries, joins, sub queries, nested where conditions, vendor engine targets and more

### Web Socket

- [SignalR](https://github.com/dotnet/aspnetcore/blob/master/src/SignalR/README.md)

ASP.NET Core SignalR is a real-time messaging library with broad compatibility and transport fallback.
