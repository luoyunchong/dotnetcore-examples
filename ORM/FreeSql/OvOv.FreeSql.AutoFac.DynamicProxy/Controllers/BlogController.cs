using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OvOv.Core.Domain;
using OvOv.Core.Models.Blogs;
using OvOv.FreeSql.AutoFac.DynamicProxy.Repositories;
using OvOv.FreeSql.AutoFac.DynamicProxy.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly BlogService _blogService;
        private readonly TagService tagService;
        private readonly IServiceScopeFactory serviceScope;
        private readonly ILifetimeScope lifetime;
        private readonly OvOvDbContext ovOvDbContext;

        public BlogController(IBlogRepository blogRepository, BlogService blogService, TagService tagService, IServiceScopeFactory serviceScope, ILifetimeScope lifetime, OvOvDbContext ovOvDbContext)
        {
            _blogRepository = blogRepository;
            this._blogService = blogService;
            this.tagService = tagService;
            this.serviceScope = serviceScope;
            this.lifetime = lifetime;
            this.ovOvDbContext = ovOvDbContext;
        }

        [HttpGet]
        public List<Blog> Get()
        {
            return _blogRepository.GetBlogs();
        }

        [HttpPost("CreateBlogByDbContext")]
        public void CreateBlogByDbContext([FromBody] CreateBlogDto createBlogDto)
        {
            _blogService.CreateBlogByDbContext(createBlogDto);
        }

        [HttpPost("CreateBlog")]
        public void CreateBlog([FromBody] CreateBlogDto createBlogDto)
        {
            _blogService.CreateBlog(createBlogDto);
        }

        [HttpPost("CreateBlogT1T2")]
        public void CreateBlogT1T2([FromBody] CreateBlogDto createBlogDto)
        {
            _blogService.CreateBlogT1T2(createBlogDto);
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

        [HttpPost("CreateBlogTransactionalTaskAsync")]
        public async Task<Blog> CreateBlogTransactionalTaskAsync([FromBody] CreateBlogDto createBlogDto)
        {
            return await _blogService.CreateBlogTransactionalTaskAsync(createBlogDto);
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
        public async Task<string> GetBlogTest()
        {
            await _blogService.GetBlogs();
            return "ok";
        }

        [HttpPost("UpdateBlogTransactionalTaskAsync")]
        public async Task<Blog> UpdateBlogTransactionalTaskAsync([FromBody] UpdateBlogDto update)
        {
            return await _blogService.UpdateBlogTransactionalTaskAsync(update);
        }

        [HttpPost("UpdateBlogDbContextTaskAsync")]
        public async Task<Blog> UpdateBlogDbContextTaskAsync([FromBody] UpdateBlogDto update)
        {
            return await _blogService.UpdateBlogDbContextTaskAsync(update);
        }

        [HttpPost("UpdateBlogTagIdentityAsync")]
        public async Task<Blog> UpdateBlogTagIdentityAsync([FromBody] UpdateBlogDto update)
        {
            return await _blogService.UpdateBlogTagIdentityAsync(update);
        }


        [HttpPost("CreateBlogUnitOfWorkAsync")]
        public async Task CreateBlogUnitOfWorkAsync([FromBody] CreateBlogDto createBlogDto)
        {
            await _blogService.CreateBlogUnitOfWorkAsync(createBlogDto);
        }

        [HttpPost("TransBlogService_CreateBlogUnitOfWorkAsync")]
        public async Task CreateBlogUnitOfWorkAsync([FromServices] TransBlogService services)
        {
            await services.CreateBlogUnitOfWorkAsync(new Blog() { Title = "create title" },
                new List<Tag> {
                    new Tag() { TagName = "tag1" },
                    new Tag() { TagName = "tag2" }
                    }
               );
        }

        [HttpDelete("TransBlogService_UpdateBlogAsync/{id}")]
        public async Task UpdateBlogAsync([FromServices] TransBlogService services, UpdateBlogDto updateBlogDto)
        {
            await services.UpdateBlogAsync(updateBlogDto);
        }
    }

}
