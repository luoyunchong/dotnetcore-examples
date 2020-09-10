using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FreeSql;
using OvOv.Core.Domain;
using OvOv.Core.Web;
using OvOv.FreeSql.AutoFac.DynamicProxy.Repositories;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Services
{
    public class TagService
    {
        private readonly IBaseRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<UserLike> _userLikeRepository;

        public TagService(IBaseRepository<Tag> tagRepository, IMapper mapper, IBaseRepository<UserLike> userLikeRepository)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _userLikeRepository = userLikeRepository;
        }
        [Transactional]
        public virtual List<int> GetArticleIds()
        {
            List<int> ids = _userLikeRepository.Select
                .ToList(r => r.ArticleId);
            return ids;
        }

        [Transactional]
        public virtual async Task<List<int>> GetArticleIdsAsync()
        {
            List<int> ids = await _userLikeRepository.Select
                .ToListAsync(r => r.ArticleId);

            return ids;

        }
        [Transactional]
        public virtual async Task<PagedResultDto<Tag>> GetAsync(PageDto pageDto)
        {
            List<Tag> tags = await _tagRepository.Select
                .OrderBy(r => r.Id).Count(out long totalCount)
                .Page(pageDto.PageNumber, pageDto.PageSize)
                .ToListAsync();

            return new PagedResultDto<Tag>(totalCount, tags);

        }
        [Transactional]
        public virtual async Task UpdateAsync(Tag tag)
        {
            Tag dataTag = await _tagRepository.Select.Where(r => r.Id == tag.Id).ToOneAsync();
            if (dataTag == null)
            {
                throw new Exception("该数据不存在");
            }
            bool exist = await _tagRepository.Select.AnyAsync(r => r.TagName == tag.TagName && r.Id != tag.Id);
            if (exist)
            {
                throw new Exception($"该标签：[{tag.TagName}]已存在");
            }

            dataTag.TagName = tag.TagName;

            await _tagRepository.UpdateAsync(dataTag);

            if (tag.TagName == "abc")
            {
                throw new Exception("abc Transactional");
            }

        }

        [Transactional]
        public virtual async Task CreateTransactionalAsync(Tag tag)
        {
            await _tagRepository.InsertAsync(tag);

            await _tagRepository.InsertAsync(
                new Tag()
                {
                    TagName = "a",
                    IsDeleted = false
                }
            );
            if (tag.TagName == "abc")
            {
                throw new Exception("abc Transactional");
            }
            await _tagRepository.InsertAsync(new Tag()
            {
                TagName = "b",
                IsDeleted = false
            });
        }

        //[Transactional(Propagation = Propagation.Required)]
        public virtual async Task CreateAsync(Tag tag)
        {
            List<Tag> tags = new List<Tag> { };
            tags.Add(new Tag()
            {
                TagName = "a",
                IsDeleted = false
            });
            tags.Add(tag);
            await _tagRepository.InsertAsync(tags);

            if (tag.TagName == "abc")
            {
                throw new Exception("exce");
            }
        }

        public virtual async Task<Tag> CreateTagIdentityAsync(Tag tag)
        {
            List<Tag> tags = new List<Tag> { };
            tags.Add(new Tag()
            {
                TagName = "a",
                IsDeleted = false
            });
            tags.Add(tag);
            await _tagRepository.InsertAsync(tags);

            if (tag.TagName == "abc")
            {
                throw new Exception("exce");
            }
            return tag;
        }
    }
}