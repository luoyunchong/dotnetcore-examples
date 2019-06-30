
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Webapi.FreeSql.Domain
{
    public class Blog
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int BlogId { get; set; }

        [Column(DbType = "varchar(50)")]
        public string Title { get; set; }
        [Column(DbType = "varchar(500)")]
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}