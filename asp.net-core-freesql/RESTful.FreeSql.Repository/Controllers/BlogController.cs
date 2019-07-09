using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTful.FreeSql.Repository.Repositories;
using RESTful.Web.Core.Domain;
using RESTful.Web.Core.Models.Blogs;

namespace RESTful.FreeSql.Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogController(BlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public List<Blog> Get()
        {
            return _blogRepository.GetBlogs();
        }

        // POST api/blog
        [HttpPost]
        public void Post([FromBody] CreateBlogDto createBlogDto)
        {
            Blog blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreateTime = DateTime.Now;
            _blogRepository.Insert(blog);
        }

        // DELETE api/blog/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _blogRepository.DeleteAsync(r => r.BlogId == id);
        }
    }
}