using OvOv.Core.Domain;
using OvOv.Core.Models.Blogs;
using System.Collections.Generic;
using System;
using AutoMapper;
using Castle.Core.Logging;
using OvOv.FreeSql.AutoFac.DynamicProxy.Repositories;
using FreeSql;
using Microsoft.Extensions.Logging;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Services
{
    public class T1Service
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<T1Service> _logger;

        public T1Service(IBlogRepository blogRepository, ITagRepository tagRepository, IMapper mapper, ILogger<T1Service> logger)
        {
            _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        public void Run(CreateBlogDto createBlogDto)
        {
            Blog blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreateTime = DateTime.Now;
            _blogRepository.Insert(blog);

            _logger.LogInformation($"T1Service-_blogRepository:{_blogRepository.UnitOfWork.GetHashCode()}");
            _logger.LogInformation($"T1Service-_tagRepository:{_tagRepository.UnitOfWork.GetHashCode()}");

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
    }
    public class T2Service
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<T2Service> _logger;
        public T2Service(IBlogRepository blogRepository, ITagRepository tagRepository, IMapper mapper, ILogger<T2Service> logger)
        {
            _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        public void Run(CreateBlogDto createBlogDto)
        {
            Blog blog = _mapper.Map<Blog>(createBlogDto);
            blog.CreateTime = DateTime.Now;
            _blogRepository.Insert(blog);

            _logger.LogInformation($"T2Service-_blogRepository:{_blogRepository.UnitOfWork.GetHashCode()}");
            _logger.LogInformation($"T2Service-_tagRepository:{_tagRepository.UnitOfWork.GetHashCode()}");

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
    }

}
