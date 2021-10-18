using AutoMapper;
using FreeSql;
using OvOv.Core.Domain;
using OvOv.Core.Models.Blogs;
using OvOv.FreeSql.Repository;
using OvOv.FreeSql.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvOv.FreeSql.Repository.Services
{

    public class BlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly UnitOfWorkManager _unitOfWorkManager;

        public BlogService(IBlogRepository blogRepository, ITagRepository tagRepository, IMapper mapper, UnitOfWorkManager unitOfWorkManager)
        {
            _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._unitOfWorkManager = unitOfWorkManager;
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
        [Transactional(Propagation = Propagation.Nested)]
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
                throw new Exception("test exception");
            }
            await _tagRepository.InsertAsync(tags);
        }
    }
}
