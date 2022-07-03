using FreeSql;
using OvOv.Core.Domain;
using OvOv.Core.Models.Blogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Services
{
    public class TransBlogService
    {
        private readonly IBaseRepository<Blog, int> _blogRepository;
        private readonly IBaseRepository<Tag, int> _tagRepository;
        private readonly UnitOfWorkManager _unitOfWorkManager;
        public TransBlogService(IBaseRepository<Blog, int> blogRepository, IBaseRepository<Tag, int> tagRepository, UnitOfWorkManager unitOfWorkManager)
        {
            _blogRepository = blogRepository;
            _tagRepository = tagRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task CreateBlogUnitOfWorkAsync(Blog blog, List<Tag> tagList)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkManager.Begin();
            try
            {
                await _blogRepository.InsertAsync(blog);
                tagList.ForEach(r =>
                {
                    r.BlogId = blog.Id;
                });
                await _tagRepository.InsertAsync(tagList);
                unitOfWork.Commit();
            }
            catch (Exception e)
            {
                //实际 可以不需要Rollback。因为IUnitOfWork内部Dispose，会把没有Commit的事务Rollback回来，但能提前Rollback
                //unitOfWork.Rollback();
                //记录日志、或继续throw;出来
            }
        }

        public async Task UpdateBlogAsync(UpdateBlogDto updateBlog)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkManager.Begin();
            try
            {
                Blog blog = _blogRepository.Select.Where(r => r.Id == updateBlog.Id).First();
                blog.Title = updateBlog.Title;
                await _blogRepository.UpdateAsync(blog);
                unitOfWork.Commit();
            }
            catch (Exception e)
            {
                unitOfWork.Rollback();
            }
        }
    }
}
