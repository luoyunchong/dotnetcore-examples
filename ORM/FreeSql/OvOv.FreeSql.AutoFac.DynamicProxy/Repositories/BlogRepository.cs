using FreeSql;
using OvOv.Core.Domain;
using System.Collections.Generic;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Repositories
{
    public class BlogRepository : DefaultRepository<Blog, int>, IBlogRepository
    {
        public BlogRepository(UnitOfWorkManager uowm) : base(uowm?.Orm, uowm)
        {
        }

        public List<Blog> GetBlogs()
        {
            return Select.Page(1, 10).ToList();
        }
    }
}
