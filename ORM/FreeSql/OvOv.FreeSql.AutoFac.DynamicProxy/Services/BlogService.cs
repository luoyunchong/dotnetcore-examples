using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OvOv.Core.Domain;
using OvOv.Core.Models.Blogs;
using OvOv.Core.Web;
using OvOv.FreeSql.AutoFac.DynamicProxy.Repositories;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Services
{

    public class BlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly TagService _tagService;

        public BlogService(IBlogRepository blogRepository, ITagRepository tagRepository, IMapper mapper, TagService tagService)
        {
            _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._tagService = tagService;
        }

        [Transactional]
        public virtual async Task<List<Blog>> GetBlogs()
        {
            var ids = _tagService.GetArticleIds();
            //var ids =  tagService.GetArticleIdsAsync();
            await _tagService.CreateAsync(new Tag() { TagName = "fff", IsDeleted = false });
            var tags = await _tagService.GetAsync(new PageDto());

            var blogs = await _blogRepository.Select
                            .Include(r => r.Classify)
                            .IncludeMany(r => r.UserLikes, r => r.Where(u => u.Status))
                            .OrderByDescending(r => r.Id).Count(out long count).ToListAsync();
            return blogs;
        }

        public void CreateBlog(CreateBlogDto createBlogDto)
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
                     PostId = blog.Id
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
            Blog blog = _mapper.Map<Blog>(updateBlogDto);
            blog.UpdateTime = DateTime.Now;
            await _blogRepository.UpdateAsync(blog);

            await _tagService.CreateAsync(
                 new Tag()
                 {
                     IsDeleted = false,
                     TagName = updateBlogDto.Title,
                     PostId = blog.Id
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
                     PostId = blog.Id
                 });
            if (updateBlogDto.Title == "abcd")
            {
                throw new Exception("ff");
            }
            return blog;
        }

    }
}
