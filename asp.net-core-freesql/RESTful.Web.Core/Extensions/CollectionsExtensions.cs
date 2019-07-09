using FreeSql;
using RESTful.Web.Core.Web;

namespace RESTful.Web.Core.Extensions
{
    public static class CollectionsExtensions
    {
        public static ISelect<T> Page<T>(this ISelect<T> source, PageDto pageDto) where T : class, new()
        {
            return source.Page(pageDto.PageNumber, pageDto.PageNumber);
        }
    }
}
