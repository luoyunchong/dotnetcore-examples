using AutoMapper;
using FreeSql;
using OvOv.Core.Domain;
using OvOv.Core.Models.Blogs;
using OvOv.Core.Web;
using OvOv.FreeSql.AutoFac.DynamicProxy.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Services
{

    public class BlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly TagService _tagService;
        private readonly UnitOfWorkManager _unitOfWorkManager;
        private readonly OvOvDbContext ovOvDbContext;
        private readonly T1Service _t1Service;
        private readonly T2Service _t2Service;
        private readonly ILogger<BlogService> _logger;

        public BlogService(IBlogRepository blogRepository, ITagRepository tagRepository, IMapper mapper, TagService tagService, UnitOfWorkManager unitOfWorkManager, OvOvDbContext ovOvDbContext, T1Service t1Service, T2Service t2Service, ILogger<BlogService> logger)
        {
            _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._tagService = tagService;
            _unitOfWorkManager = unitOfWorkManager;
            this.ovOvDbContext = ovOvDbContext;
            _t1Service = t1Service;
            _t2Service = t2Service;
            _logger = logger;
        }

        [Transactional]
        public virtual async Task<List<Blog>> GetBlogs()
        {
            await _tagService.CreateAsync(new Tag() { TagName = "fff", IsDeleted = false });
            var tags = await _tagService.GetAsync(new PageDto());

            var blogs = await _blogRepository.Select
                            .Include(r => r.Classify)
                            .OrderByDescending(r => r.Id).Count(out long count).ToListAsync();
            return blogs;
        }
        public void CreateBlogByDbContext(CreateBlogDto createBlogDto)
        {

            Blog blog = _mapper.Map<Blog>(createBlogDto);
            ovOvDbContext.Set<Blog>().Add(blog);
            ovOvDbContext.SaveChanges();
        }

        public void CreateBlogT1T2(CreateBlogDto createBlogDto)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkManager.Begin();
            //try
            //{
            _logger.LogInformation($"BlogService-CreateBlogT1T2:{unitOfWork.GetHashCode()}");


            _t1Service.Run(createBlogDto);
            _t2Service.Run(createBlogDto);

            unitOfWork.Commit();
            //}
            //catch (Exception)
            //{
            //    unitOfWork.Rollback();
            //    throw;
            //}
        }
        public void CreateBlog(CreateBlogDto createBlogDto)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkManager.Begin();
            //try
            //{
            Blog blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreateTime = DateTime.Now;
            _blogRepository.Insert(blog);

            List<Tag> tags = new List<Tag>();
            createBlogDto.Tags.ForEach(r =>
            {
                tags.Add(new Tag { TagName = r });
            });
            if (createBlogDto.Title == "abc")
            {
                throw new Exception("test exception");
            }
            _tagRepository.Insert(tags);

            unitOfWork.Commit();
            //}
            //catch (Exception)
            //{
            //    unitOfWork.Rollback();
            //    throw;
            //}
        }

        /// <summary>
        /// 当出现异常时，不会插入数据
        /// </summary>
        /// <param name="createBlogDto"></param>
        [Transactional]
        public virtual void CreateBlogTransactional(CreateBlogDto createBlogDto)
        {
            Blog blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreateTime = DateTime.Now;
            _blogRepository.Insert(blog);

            List<Tag> tags = new List<Tag>();
            createBlogDto.Tags.ForEach(r =>
            {
                tags.Add(new Tag { TagName = r });
            });
            if (createBlogDto.Title == "abc")
            {
                throw new Exception("test exception");
            }
            _tagRepository.Insert(tags);
        }

        public async Task CreateBlogAsync(CreateBlogDto createBlogDto)
        {
            Blog blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreateTime = DateTime.Now;
            await _blogRepository.InsertAsync(blog);

            List<Tag> tags = new List<Tag>();
            createBlogDto.Tags.ForEach(r =>
            {
                tags.Add(new Tag { TagName = r });
            });
            if (createBlogDto.Title == "abc")
            {
                throw new Exception("test exception");
            }
            await _tagRepository.InsertAsync(tags);
        }

        [Transactional]
        public virtual async Task CreateBlogTransactionalAsync(CreateBlogDto createBlogDto)
        {
            Blog blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreateTime = DateTime.Now;
            await _blogRepository.InsertAsync(blog);

            List<Tag> tags = new List<Tag>();
            createBlogDto.Tags.ForEach(r =>
            {
                tags.Add(new Tag { TagName = r });
            });
            if (createBlogDto.Title == "abc")
            {
                throw new Exception("test exception CreateBlogTransactionalAsync");
            }
            await _tagRepository.InsertAsync(tags);
        }

        /// <summary>
        /// 嵌套事务测试
        /// </summary>
        /// <param name="createBlogDto"></param>
        /// <returns></returns>
        [Transactional]
        public virtual async Task<Blog> CreateBlogTransactionalTaskAsync(CreateBlogDto createBlogDto)
        {
            Blog blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreateTime = DateTime.Now;
            await _blogRepository.InsertAsync(blog);

            if (createBlogDto.Title == "abc")
            {
                throw new Exception("test exception CreateBlogTransactionalAsync");
            }
            //await _tagService.CreateTransactionalAsync(
            //    new Tag()
            //    {
            //        IsDeleted = false,
            //        TagName = "tagName",
            //        PostId = blog.Id
            //    });

            await _tagService.CreateAsync(
                 new Tag()
                 {
                     IsDeleted = false,
                     TagName = "tagName",
                 });
            return blog;
        }

        /// <summary>
        /// Update事务测试
        /// </summary>
        /// <param name="createBlogDto"></param>
        /// <returns></returns>
        [Transactional]
        public virtual async Task<Blog> UpdateBlogTransactionalTaskAsync(UpdateBlogDto updateBlogDto)
        {
            Blog blog = await _blogRepository.FindAsync(updateBlogDto.Id);
            _mapper.Map(updateBlogDto, blog);
            await _blogRepository.UpdateAsync(blog);

            blog.UpdateTime = DateTime.Now;
            blog.Title = updateBlogDto.Title + Guid.NewGuid().ToString();
            await _blogRepository.UpdateAsync(blog);

            await _tagService.CreateAsync(
                 new Tag()
                 {
                     IsDeleted = false,
                     TagName = updateBlogDto.Title,
                     BlogId = blog.Id
                 });
            if (updateBlogDto.Title == "abcd")
            {
                throw new Exception("ff");
            }
            return blog;
        }

        /// <summary>
        /// DBContext嵌套事务
        /// </summary>
        /// <param name="updateBlogDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [Transactional]
        public virtual async Task<Blog> UpdateBlogDbContextTaskAsync(UpdateBlogDto updateBlogDto)
        {
            var blogDbSet = ovOvDbContext.Set<Blog>();
            Blog blog = await blogDbSet.Where(r => r.Id == updateBlogDto.Id).FirstAsync();
            _mapper.Map(updateBlogDto, blog);
     //       byte[] b = Encoding.UTF8.GetBytes(blog.Version);

            await blogDbSet.UpdateAsync(blog);
            ovOvDbContext.SaveChanges();
            blog.UpdateTime = DateTime.Now;
            blog.Title = updateBlogDto.Title + Guid.NewGuid().ToString();
            await blogDbSet.UpdateAsync(blog);
            ovOvDbContext.SaveChanges();
            await _tagService.CreateAsync(
                 new Tag()
                 {
                     IsDeleted = false,
                     TagName = updateBlogDto.Title,
                     BlogId = blog.Id
                 });
            if (updateBlogDto.Title == "abcd")
            {
                throw new Exception("ff");
            }
            return blog;
        }


        /// <summary>
        /// 有返回值
        /// </summary>
        /// <param name="updateBlogDto"></param>
        /// <returns></returns>
        [Transactional]
        public virtual async Task<Blog> UpdateBlogTagIdentityAsync(UpdateBlogDto updateBlogDto)
        {
            Blog blog = _mapper.Map<Blog>(updateBlogDto);
            blog.UpdateTime = DateTime.Now;
            await _blogRepository.UpdateAsync(blog);

            await _tagService.CreateTagIdentityAsync(
                 new Tag()
                 {
                     IsDeleted = false,
                     TagName = updateBlogDto.Title,
                     BlogId = blog.Id
                 });
            if (updateBlogDto.Title == "abcd")
            {
                throw new Exception("ff");
            }
            return blog;
        }

        public virtual async Task CreateBlogUnitOfWorkAsync(CreateBlogDto createBlogDto)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkManager.Begin())
            {
                try
                {
                    Blog blog = _mapper.Map<Blog>(createBlogDto);
                    blog.CreateTime = DateTime.Now;
                    await _blogRepository.InsertAsync(blog);

                    List<Tag> tags = new List<Tag>();
                    createBlogDto.Tags.ForEach(r =>
                    {
                        tags.Add(new Tag { TagName = r });
                    });
                    if (createBlogDto.Title == "abc")
                    {
                        throw new Exception("test exception CreateBlogTransactionalAsync");
                    }
                    await _tagRepository.InsertAsync(tags);
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                }
            }
        }
    }
}
