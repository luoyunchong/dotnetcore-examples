<!------->
<!--title: 3.FreeSql在ASP.NTE Core WebApi中如何使用的教程-->
<!--date: 2019-6-30 01:52:22-->
<!--updated: 2019-7-1 22:22:22-->
<!--# top: 近期更新-->
<!--tags:-->
<!--- FreeSql-->
<!--category:-->
<!--- 重新出发-->
<!------->

## 文章概述
主要在介绍FreeSql在ASP.NTE Core WebApi中如何使用的过程，完成一个最简单的博客系统的后端接口。

## FreeSql 简介
国人写的一个功能强大的ORM,FreeSql 支持 MySql/SqlServer/PostgreSQL/Oracle/Sqlite，特点：轻量级、可扩展、基于 .NET Standard 跨平台。

### 参考
- FreeSql github [https://github.com/2881099/FreeSql](https://github.com/2881099/FreeSql) 

- [关于.net core cli中如何使用dotnet new](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22)
- [使用 ASP.NET Core 创建 Web API](https://docs.microsoft.com/zh-cn/aspnet/core/web-api/?view=aspnetcore-2.2)
- [Swagger/OpenAPI 生成接口文档](https://docs.microsoft.com/zh-cn/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.2)
- [Swagger GitHub (Swashbuckle.AspNetCore)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
### 项目准备
- Mysql 5.6
- Visual Studio 2019或2017、Visual Studio code
- .NET Core 2.2+
- PowerShell
- 懂点mvc，该教程不会教你如何使用 ASP .NET Core MVC、RESTful

<!-- more -->

创建一个webapi 的项目，起名为RESTful.FreeSql

```PowerShell
PS dotnetcore-examples\asp.net-core-freesql> dotnet new webapi -n RESTful.FreeSql
The template "ASP.NET Core Web API" was created successfully.
PS dotnetcore-examples\asp.net-core-freesql> cd .\RESTful.FreeSql\
PS dotnetcore-examples\asp.net-core-freesql\RESTful.FreeSql> dotnet run

info: Microsoft.Hosting.Lifetime[0]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: D:\code\github\dotnetcore-examples\asp.net-core-freesql\RESTful.FreeSql
```

打开浏览器 https://localhost:5001 会出现404

请打开这个地址 https://localhost:5001/api/values ，可看到如下内容。
~~~
["value1","value2"]
~~~

接下来我们来集成FreeSql，我们以最简单的命令和说明，详细内容去官网看具体内容

- 官网文档 [http://freesql.net/doc](http://freesql.net/doc)
## Install
要先cd到RESTful.FreeSql目录中。
~~~PowerShell
PS \asp.net-core-freesql\RESTful.FreeSql> dotnet add package FreeSql
PS \asp.net-core-freesql\RESTful.FreeSql> dotnet add package FreeSql.Provider.MySql
~~~


## code first
- [关于CodeFirst，官方文档的介绍](https://github.com/2881099/FreeSql/blob/master/Docs/codefirst.md)

代码优先，使用过EntityFramework的应该很清楚这一概念，我的理解就是：在分析数据库表关系时，不通过在数据库中设计表，而是直接在代码中声明对应的类，使用导航属性代替外键关联，通过数据表字段与C#中的类库对应，从而自动生成数据表。


## db first 
数据库优先：需求分析后，直接设计数据库，通过数据库中的表，直接生成代码，类。

## 开始
### 分析需求

我们以code first 为示例，学习如何使用freesql，实现一个简单的博客。将表内容分为博客表（Blog)和评论表（Post)

#### Blog 表

|字段名 | 字段类型| 说明|
|---|---|---|
|BlogId |int|博客id|
|Title  |varchar(50)|博客标题|
|Content  |varchar(500)|博客内容|
|CreateTime  |DateTime|发布时间|

#### Post 表

|字段名 | 字段类型| 说明|
|---|---|---|
|PostId |int|评论id|
|ReplyContent   |varchar(50)|标题|
|BlogId    |int|博客id|
|ReplyTime    |DateTime|回复时间|

建一个Domain文件夹,用于存放数据库表中对应的实体类。
### 关于

#### 1. Column属性介绍，大家可以看[源码，解析](https://github.com/2881099/FreeSql/blob/f8c3608fdac2933b528605cc46b21b71c79eaacb/FreeSql/DataAnnotations/ColumnAttribute.cs)

1). 比如：Blog表中指定了Title为varchar(50),我们如何通过代码指定了主键，唯一值，字形。
```c#
    public class Blog
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int BlogId { get; set; }
        [Column(DbType = "varchar(50)")]
        public string Title { get; set; }
    }
```
2). Column的命名空间在
```c#
using FreeSql.DataAnnotations;
```
更多属性介绍
|字段 | 备注|
|---|---|
|Name|数据库列名|
|OldName|指定数据库旧的列名，修改实体属性命名时，同时设置此参数为修改之前的值，CodeFirst才可以正确修改数据库字段；否则将视为【新增字段】|
|DbType|数据库类型，如： varchar(255)|
|IsPrimary|主键|
|IsIdentity|自增标识|
|IsNullable|是否可DBNull|
|IsIgnore|忽略此列，不迁移、不插入|
|IsVersion|设置行锁（乐观锁）版本号，每次更新累加版本号，若更新整个实体时会附带当前的版本号判断（修改失败时抛出异常）|
|DbDefautValue|数据库默认值|
|MapType|类型映射，比如：可将 enum 属性映射成 typeof(string)|
|Uniques| 唯一键，在多个属性指定相同的标识，代表联合键；可使用逗号分割多个 UniqueKey 名。|

#### 2. Table 的使用：用于在类的上面指定这个表的属性

```csharp
[Table(Name = "t_blog")]
public class Blog {
  //...
}
```
更多属性介绍
|字段 | 备注|
|---|---|
|Name|数据库表名|
|OldName |指定数据库旧的表名，修改实体命名时，同时设置此参数为修改之前的值，CodeFirst才可以正确修改数据库表；否则将视为【创建新表】|
|SelectFilter |查询过滤SQL，实现类似 a.IsDeleted = 1 功能|
|DisableSyncStructure|禁用 CodeFirst 同步结构迁移|

#### 3. 其他的还是看 https://github.com/2881099/FreeSql/blob/master/Docs/codefirst.md

#### Blog.cs
~~~c#
using FreeSql.DataAnnotations;
using System;

namespace RESTful.FreeSql.Domain
{
    public class Blog
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int BlogId { get; set; }
        [Column(DbType = "varchar(50)")]
        public string Title { get; set; }
        [Column(DbType = "varchar(500)")]
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }


    }
}
~~~

#### Post.cs
~~~C#

using FreeSql.DataAnnotations;
using System;

namespace RESTful.FreeSql.Domain
{
    public class Post
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int PostId { get; set; }
        [Column(DbType = "varchar(50)")]
        public string ReplyContent { get; set; }
        public int BlogId { get; set; }
        public DateTime ReplyTime { get; set; }
        public Blog Blog { get; set; }
    }
}
~~~



#### Startup.cs
非全部代码，这里注意点：要先在mysql中创建数据库**FreeSql_Blog**，否则一直提示**主库xxxxx**,官网未找到相关描述。

这里初始化FreeSql，并使用单例模式，注入到默认的依赖中，这样在Controller中即可直接注入。
~~~c#
namespace RESTful.FreeSql
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Fsql = new FreeSqlBuilder()
                        .UseConnectionString(DataType.MySql, @"Data Source=127.0.0.1;Port=3306;User ID=root;Password=123456;Initial Catalog=FreeSql_Blog;Charset=utf8;SslMode=none;Max pool size=10")
                        .UseAutoSyncStructure(true)
                        .Build();
        }

        public IFreeSql Fsql { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFreeSql>(Fsql);

        }
    }
}
~~~

#### BlogController
在controllers文件夹新建一个控制器BlogController

~~~c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using Microsoft.AspNetCore.Mvc;
using RESTful.FreeSql.Domain;

namespace RESTful.FreeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        // GET api/Blog

        IFreeSql _fsql;
        public BlogController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Blog>> Get()
        {
            List<Blog> blogs = _fsql.Select<Blog>().OrderByDescending(r => r.CreateTime).ToList();

            return blogs;
        }

        // GET api/blog/5
        [HttpGet("{id}")]
        public ActionResult<Blog> Get(int id)
        {
            return _fsql.Select<Blog>(id).ToOne();
        }


        // DELETE api/blog/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _fsql.Delete<Blog>(new { BlogId = id }).ExecuteAffrows();
        }
    }
}
~~~

重新运行，打开地址 http://localhost:5001/api/blog 会发现数据库中生成了表blog，这时候表post并没有生成。所以我们判断，只有在访问到实体类才检查是否存在表结构，然后执行相应的处理。

手动向blog表中加一些数据，然后再次请求 
- http://localhost:5001/api/blog， 可看到相应的数据。
- http://localhost:5001/api/blog/1  可得到单个数据。


#### 自动同步实体结构【开发环境必备】
此功能默认为开启状态，发布正式环境后，请修改此设置
~~~
Fsql = new FreeSqlBuilder()
          .UseConnectionString(DataType.MySql, @"连接字符串")
          .UseAutoSyncStructure(true)
          .Build();
                      
//UseAutoSyncStructure(true/false)【开发环境必备】自动同步实体结构到数据库，程序运行中检查实体表是否存在，然后创建或修改

// 也可使用此方法指定是否自动同步结构。                  
Fsql.CodeFirst.IsAutoSyncStructure = true;
~~~

