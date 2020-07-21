using System.Collections.Generic;
using FreeSql;
using OvOv.Core.Domain;

namespace OvOv.FreeSql.Repository.Repositories
{
    public interface IBlogRepository : IBaseRepository<Blog, int>
    {

        List<Blog> GetBlogs();
    }
}
