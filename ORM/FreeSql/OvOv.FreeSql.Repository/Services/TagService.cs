using System;
using System.Threading.Tasks;
using AutoMapper;
using OvOv.Core.Domain;
using OvOv.FreeSql.Repository.Repositories;

namespace OvOv.FreeSql.Repository.Services
{
    public interface ITagService
    {
        Task CreateAsync(Tag tag);
        Task UpdateAsync(Tag tag);
    }

    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [Transactional]
        public async Task UpdateAsync(Tag tag)
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

        }

        public async Task CreateAsync(Tag tag)
        {
            await _tagRepository.InsertAsync(tag);
        }
    }
}