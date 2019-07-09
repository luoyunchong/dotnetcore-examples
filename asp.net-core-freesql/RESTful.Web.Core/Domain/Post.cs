using System;
using FreeSql.DataAnnotations;

namespace RESTful.Web.Core.Domain
{
    public class Post : ISoftDelete
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int PostId { get; set; }

        [Column(DbType = "varchar(50)")]
        public string ReplyContent { get; set; }
        public int BlogId { get; set; }
        public DateTime ReplyTime { get; set; }
        public virtual Blog Blog { get; set; }
        public bool IsDeleted { get; set; }
    }
}