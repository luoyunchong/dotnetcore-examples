using System;
using System.Collections;
using System.Collections.Generic;
using FreeSql.DataAnnotations;

namespace OvOv.Core.Domain
{
    public class Post : ISoftDelete
    {
        public Post()
        {
        }

        public Post(string replyContent, DateTime replyTime, bool isDeleted)
        {
            ReplyContent = replyContent ?? throw new ArgumentNullException(nameof(replyContent));
            ReplyTime = replyTime;
            IsDeleted = isDeleted;
        }

        [Column(IsIdentity = true, IsPrimary = true)]
        public int PostId { get; set; }

        [Column(StringLength = 50)]
        public string ReplyContent { get; set; }
        public int BlogId { get; set; }
        public DateTime ReplyTime { get; set; }
        public virtual Blog Blog { get; set; }
        public bool IsDeleted { get; set; }
    }
}