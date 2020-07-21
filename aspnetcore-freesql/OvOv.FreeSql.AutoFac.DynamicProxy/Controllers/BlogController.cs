using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OvOv.Core.Domain;
using OvOv.Core.Models.Blogs;
using OvOv.Core.Web;
using OvOv.FreeSql.AutoFac.DynamicProxy.Repositories;
using OvOv.FreeSql.AutoFac.DynamicProxy.Services;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly BlogService _blogService;
        private readonly TagService tagService;

        public BlogController(IBlogRepository blogRepository, BlogService blogService, TagService tagService)
        {
            _blogRepository = blogRepository;
            this._blogService = blogService;
            this.tagService = tagService;
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
            _blogService.CreateBlog(createBlogDto);
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
            await _blogService.CreateBlogAsync(createBlogDto);
        }

        [HttpPost("CreateBlogTransactionalAsync")]
        public async Task CreateBlogTransactionalAsync([FromBody] CreateBlogDto createBlogDto)
        {
            await _blogService.CreateBlogTransactionalAsync(createBlogDto);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _blogRepository.DeleteAsync(r => r.Id == id);
        }



        [HttpGet("blog-tag")]
        public async Task<List<Blog>> GetBlogTagAsync()
        {
            return await _blogService.GetBlogs();
        }


        [HttpGet("blog-tag-test")]
        public string GetBlogTest()
        {
            _blogService.GetBlogs();
            return "ok";
        }
    }


}