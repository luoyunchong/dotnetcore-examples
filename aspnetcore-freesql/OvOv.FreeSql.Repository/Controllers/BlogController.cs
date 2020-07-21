using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Mvc;
using OvOv.Core.Domain;
using OvOv.Core.Models.Blogs;
using OvOv.FreeSql.Repository.Repositories;
using OvOv.FreeSql.Repository.Services;

namespace OvOv.FreeSql.Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly BlogService blogService;

        public BlogController(IBlogRepository blogRepository, BlogService blogService)
        {
            _blogRepository = blogRepository;
            this.blogService = blogService;
        }

        [HttpGet]
        public List<Blog> Get()
        {
            return _blogRepository.GetBlogs();
        }

        // POST api/blog
        [HttpPost("CreateBlog")]
        public void CreateBlog([FromBody] CreateBlogDto createBlogDto)
        {
            blogService.CreateBlog(createBlogDto);
        }

        /// <summary>
        /// 当出现异常时，不会插入数据
        /// </summary>
        /// <param name="createBlogDto"></param>
        [HttpPost("CreateBlogTransactional")]
        public void CreateBlogTransactional([FromBody] CreateBlogDto createBlogDto, [FromServices] BlogService blogService2)
        {
            blogService2.CreateBlogTransactional(createBlogDto);
        }

        [HttpPost("CreateBlogAsync")]
        public async Task CreateBlogAsync([FromBody] CreateBlogDto createBlogDto)
        {
            await blogService.CreateBlogAsync(createBlogDto);
        }

        [HttpPost("CreateBlogTransactionalAsync")]
        public async Task CreateBlogTransactionalAsync([FromBody] CreateBlogDto createBlogDto)
        {
            await blogService.CreateBlogTransactionalAsync(createBlogDto);
        }

        // DELETE api/blog/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _blogRepository.DeleteAsync(r => r.Id == id);
        }
    }


}