using System.Collections.Generic;
using FreeSql;
using OvOv.Core.Domain;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Repositories
{
    public class BlogRepository : DefaultRepository<Blog,int>, IBlogRepository
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
