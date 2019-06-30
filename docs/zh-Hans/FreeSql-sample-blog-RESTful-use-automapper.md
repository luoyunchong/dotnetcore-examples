<!-- ---
title: 4. 使用RESTful、FreeSql构建简单的博客系统-集成AutoMapper
date: 2019-6-30 15:39:22
# updated: 2019-7-1 22:22:22
# top: 近期更新
tags:
- FreeSql
category:
- 重新出发
---  -->

## 文章概述
本文使用ASP .NET Core的WEB API，构建一个RESTful风格的接口，使用Freesql访问MySQL数据库，实现二个表的简单博客，并集成AutoMapper。

接上一篇 
- [FreeSql在ASP.NTE Core WebApi中如何使用的教程](FreeSql-in-asp.net-core-webapi-how-to-use.md)

- 项目源码 [https://github.com/luoyunchong/dotnetcore-examples/tree/master/WebAPI-FreeSql](https://github.com/luoyunchong/dotnetcore-examples/tree/master/WebAPI-FreeSql)

<!-- more -->

## Dto作用

当我们使用RESTful提供接口时，比如创建一个博客，修改一下博客内容时，他们的参数是有区别的。良好的设计应该是



创建一个博客
~~~json
POST /api/blog
data:
{
  "title": "string",
  "content": "string"
}
~~~
修改一个博客内容
~~~json
PUT /api/blog
data:
{
  "blogId":"int",
  "title": "string",
  "content": "string"
}
~~~
但一个blog 实体如下

~~~c#
    public class Blog
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
~~~

如果我们以Blog作为controllers中的参数时

~~~c#
       // POST api/blog
        [HttpPost]
        public void Post([FromBody] Blog blog)
        {
            blog.CreateTime=DateTime.Now;
            _fsql.Insert<Blog>(blog).ExecuteAffrows();
        }
~~~
这时修改swagger显示的默认参数是
~~~json
{
  "blogId": 0,
  "title": "string",
  "content": "string",
  "createTime": "2019-06-30T07:33:05.524Z",
  "posts": [
    {
      "postId": 0,
      "replyContent": "string",
      "blogId": 0,
      "replyTime": "2019-06-30T07:33:05.524Z"
    }
  ]
}
~~~

如果我们不传递createTime，会出现异常，应该createTime是DateTime，不能为null，只有DateTime?才能为null,有?为可空类型。

所以我们应该为POST方式传递过来时，新建一个实体类，我们称之为DTO(Data Transfer Object)，即数据传输对象，因为createTime即使传递，后端为他赋了值，前台传了也无效。有了DTO，这样可让前端清楚他们应该传递的参数，而不会出现没有作用的参数。

在根目录创建Models/Blogs文件夹，在Blogs文件夹中创建

### CreateBlogDto.cs

```c#
namespace Webapi.FreeSql.Models.Blogs
{
    public class CreateBlogDto
    {
        public string Title { get; set; }
        public string Content { get; set; }

    }
}

```
### UpdateBlogDto.cs

```c#
namespace Webapi.FreeSql.Models.Blogs
{
    public class UpdateBlogDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}


```
有了Dto后，我们会发现了新的问题，往数据库插入时，往往使用了一些ORM,他们只支持原本的实体类，如Blog，Post。但不支持CreateBlogDto、UpdateBlogDto,我们可以手动，将一个类的值，赋值给另一个类。
如
```c#
    CreateBlogDto createBlogDto = new CreateBlogDto()
    {
        Title = "我是title",
        Content = "我是content"
    };

    Blog newBlog=new Blog()
    {
        Title = createBlogDto.Title,
        Content = createBlogDto.Content
    };
```
现在只是非常简单的二个属性，我们还能忍受，但如果是十个属性、而且有着大量的类与类之间的转换呢。这时修改AutoMapper就闪亮登场了。

## AutoMapper
> 作用：A convention-based object-object mapper.

我们是在ASP .NET Core下使用AutoMapper [官网介绍，如何依赖注入中使用](https://automapper.readthedocs.io/en/latest/Dependency-injection.html)

### Setup
先cd到dotnetcore-examples\WebAPI-FreeSql\Webapi.FreeSql目录
```PowerShell
PS > dotnet add package AutoMapper
PS > dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 6.1.1
```
在StartUp.cs中的ConfigureServices配置如下
```
   public void ConfigureServices(IServiceCollection services)
    {
        // .... Ignore code before this
        
        //AddAutoMapper会去找继承Profile的类，
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // .... Ignore code after this
    }
```

### Adding Profiles

AutoMapper/BlogProfile.cs
```c#
using AutoMapper;
using Webapi.FreeSql.Domain;
using Webapi.FreeSql.Models.Blogs;

namespace Webapi.FreeSql.AutoMapper
{
    public class BlogProfile : Profile
    {
        public BlogProfile() 
        {
            CreateMap<CreateBlogDto, Blog>();
            CreateMap<UpdateBlogDto, Blog>();
        }
    }
}
```

AutoMapper/BlogProfile.cs
```c#
using AutoMapper;
using Webapi.FreeSql.Domain;
using Webapi.FreeSql.Models.Blogs;

namespace Webapi.FreeSql.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostDto,Post>();
        }
    }
}
```

Models/Posts/SearchPostDto.cs根据博客id，得到分页的评论时，集成分页类
```c#
using Webapi.FreeSql.Web;

namespace Webapi.FreeSql.Models.Posts
{
    public class SearchPostDto:PageDto
    {
        public int BlogId { get; set; }
    }
}

```

Controlers/BlogController.cs文件中，注入IMapper,
```c#
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Webapi.FreeSql.Domain;
using Webapi.FreeSql.Models.Blogs;
using Webapi.FreeSql.Web;

namespace Webapi.FreeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        // GET api/Blog

        private readonly IFreeSql _fsql;
        private readonly IMapper _mapper;
        public BlogController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        /// <summary>
        /// 博客列表页 
        /// </summary>
        /// <param name="pageDto">分页参数</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<PagedResultDto<Blog>> Get([FromQuery]PageDto pageDto)
        {
            List<Blog> blogs = _fsql.Select<Blog>().OrderByDescending(r => r.CreateTime).Page(pageDto.PageNumber, pageDto.PageSize).ToList();
            long count = _fsql.Select<Blog>().Count();
            return new PagedResultDto<Blog>(count, blogs);
        }

        // GET api/blog/5
        [HttpGet("{id}")]
        public ActionResult<Blog> Get(int id)
        {
            // eg.1 return _fsql.Select<Blog>().Where(a => a.Id == id).ToOne();
            // eg.2
            return _fsql.Select<Blog>(id).ToOne();
        }

        // POST api/blog
        [HttpPost]
        public void Post([FromBody] CreateBlogDto createBlogDto)
        {
            Blog blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreateTime = DateTime.Now;
            _fsql.Insert<Blog>(blog).ExecuteAffrows();
        }

        // PUT api/blog
        [HttpPut]
        public void Put([FromBody] UpdateBlogDto updateBlogDto)
        {

            //eg.1 更新指定列
            //_fsql.Update<Blog>(updateBlogDto.BlogId).Set(a => new Blog()
            //{
            //    Title = updateBlogDto.Title,
            //    Content = updateBlogDto.Content
            //}).ExecuteAffrows();

            //eg.2将这个实体更新到数据库中。当更新时，会把其他列的值，如CreateTime也更新掉。
            //使用IgnoreColumns可忽略某一些列。

            Blog blog = _mapper.Map<Blog>(updateBlogDto);
            _fsql.Update<Blog>().SetSource(blog).IgnoreColumns(r => r.CreateTime).ExecuteAffrows();
        }

        // DELETE api/blog/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _fsql.Delete<Blog>(new { BlogId = id }).ExecuteAffrows();
        }
    }
}
```

Controlers/BlogController.cs文件中，注入IMapper,
```c#
using FreeSql;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Webapi.FreeSql.Domain;
using Webapi.FreeSql.Models.Posts;
using Webapi.FreeSql.Web;

namespace Webapi.FreeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        // GET: api/Post
        private readonly IFreeSql _fsql;
        private readonly IMapper _mapper;
        public PostController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        /// <summary>
        /// 根据博客id、分页条件查询评论信息
        /// </summary>
        /// <param name="searchPostDto"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedResultDto<Post> Get(SearchPostDto searchPostDto)
        {
            ISelect<Post> selectPost = _fsql
                .Select<Post>()
                .Where(r => r.BlogId == searchPostDto.BlogId);

            List<Post> posts = selectPost.OrderByDescending(r => r.ReplyTime)
                .Page(searchPostDto.PageNumber, searchPostDto.PageSize)
                .ToList();

            long total = selectPost.Count();

            return new PagedResultDto<Post>(total, posts);
        }

        // GET: api/Post/5
        [HttpGet("{id}", Name = "Get")]
        public Post Get(int id)
        {
            return _fsql.Select<Post>().Where(a => a.PostId == id).ToOne();
        }

        // POST: api/Post
        [HttpPost]
        public void Post([FromBody] CreatePostDto createPostDto)
        {
            Post post = _mapper.Map<Post>(createPostDto);
            post.ReplyTime = DateTime.Now;
            _fsql.Insert(post).ExecuteAffrows();
        }


        // DELETE: api/Post/
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _fsql.Delete<Post>(new Post { PostId = id }).ExecuteAffrowsAsync();
        }
    }
}

```

### 参考
- 建议大家先看官网 [http://automapper.org/](http://automapper.org/)
- 开源地址 [https://github.com/AutoMapper/AutoMapper](https://github.com/AutoMapper/AutoMapper)
- Getting-started 文档 [https://automapper.readthedocs.io/en/latest/Getting-started.html#what-is-automapper](https://automapper.readthedocs.io/en/latest/Getting-started.html#what-is-automapper)
