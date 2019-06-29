
using FreeSql.DataAnnotations;

namespace Webapi.FreeSql.Domain
{
    public class Post
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}