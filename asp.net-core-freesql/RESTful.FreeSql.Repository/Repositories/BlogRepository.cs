using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FreeSql;
using RESTful.Web.Core.Domain;

namespace RESTful.FreeSql.Repository.Repositories
{
    public class BlogRepository : BaseRepository<Blog>
    {
        public BlogRepository(IFreeSql fsql, Expression<Func<Blog, bool>> filter = null, Func<string, string> asTable = null) : base(fsql, filter, asTable)
        {
        }

        public List<Blog> GetBlogs()
        {
            return Select.Page(1, 10).ToList();
        }


    }
}
