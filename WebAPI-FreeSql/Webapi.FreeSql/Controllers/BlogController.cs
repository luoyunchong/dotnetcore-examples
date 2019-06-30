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

        private void Test()
        {
            CreateBlogDto createBlogDto = new CreateBlogDto()
            {
                Title = "我是title",
                Content = "我是content"
            };

            Blog newBlog = new Blog()
            {
                Title = createBlogDto.Title,
                Content = createBlogDto.Content
            };

        }
    }
}