
## FreeSql在ASP.NTE Core WebApi中如何使用的教程

- FreeSql github [https://github.com/2881099/FreeSql](https://github.com/2881099/FreeSql) 
- [关于.net core cli中如何使用dotnet new](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22)

## FreeSql 简介
国人写的一个功能强大的ORM,FreeSql 支持 MySql/SqlServer/PostgreSQL，特点：轻量级、可扩展、基于 .NET Standard 跨平台。
### 项目准备
- Mysql 5.6
- Visual Studio 2019或2017、Visual Studio code
- .NET Core 2.2+
- PowerShell
- 懂点mvc，该教程不会教你如何使用 ASP .NET Core MVC


创建一个webapi 的项目，起名为WebAPI.FreeSql

```PowerShell
PS dotnetcore-examples\WebAPI-FreeSql> dotnet new webapi -n Webapi.FreeSql
The template "ASP.NET Core Web API" was created successfully.
PS dotnetcore-examples\WebAPI-FreeSql> cd .\Webapi.FreeSql\
PS dotnetcore-examples\WebAPI-FreeSql\Webapi.FreeSql> dotnet run

info: Microsoft.Hosting.Lifetime[0]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: D:\code\github\dotnetcore-examples\WebAPI-FreeSql\Webapi.FreeSql
```

打开浏览器 https://localhost:5001 会出现404

请打开这个地址 https://localhost:5001/api/values ，可看到如下内容。
~~~
["value1","value2"]
~~~

接下来我们来集成FreeSql，我们以最简单的命令和说明，详细内容去官网看具体内容

- 官网文档 [http://freesql.net/doc](http://freesql.net/doc)
## Install
要先cd到Webapi.FreeSql目录中。
~~~PowerShell
PS \WebAPI-FreeSql\Webapi.FreeSql> dotnet add package FreeSql
PS \WebAPI-FreeSql\Webapi.FreeSql> dotnet add package FreeSql
~~~


## code first
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
|Rating  |int|评级|
|Url  |varchar(500)|地址|

#### Post 表

|字段名 | 字段类型| 说明|
|---|---|---|
|PostId |int|评论id|
|Title   |varchar(50)|标题|
|Content   |varchar(200)|内容|
|BlogId    |int|博客id|

建一个Domain文件夹,用于存放数据库表中对应的实体类。

#### Blog.cs
~~~c#
namespace Webapi.FreeSql.Domain
{
    public class Blog
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
    }
}
~~~

#### Post.cs
~~~C#
namespace Webapi.FreeSql.Domain
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
~~~


#### Startup.cs
非全部代码，这里注意点：要先在mysql中创建数据库**FreeSql_Blog**，否则一直提示**主库xxxxx**,官网未找到相关描述。
~~~c#
namespace Webapi.FreeSql
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
using Webapi.FreeSql.Domain;

namespace Webapi.FreeSql.Controllers
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
            List<Blog> blogs = _fsql.Select<Blog>().Where(r => r.Rating > 0.1).OrderBy(r => r.Url).ToList();

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

[CodeFirst官网介绍](https://github.com/2881099/FreeSql/blob/master/Docs/codefirst.md)

### 自动同步实体结构【开发环境必备】
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