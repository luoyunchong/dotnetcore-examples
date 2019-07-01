using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using Webapi.FreeSql.Web;

namespace Webapi.FreeSql.Extensions
{
    public static class CollectionsExtensions
    {
        public static ISelect<T> Page<T>(this ISelect<T> source, PageDto pageDto) where T : class, new()
        {
            return source.Page(pageDto.PageNumber, pageDto.PageNumber);
        }
    }
}
