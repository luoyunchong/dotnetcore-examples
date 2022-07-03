using FreeSql.DataAnnotations;
using System;

namespace OvOv.Core.Domain
{
    public class Blog : ISoftDelete
    {
        public Blog()
        {
        }

        public Blog(string title, string content, DateTime createTime, bool isDeleted)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            CreateTime = createTime;
            IsDeleted = isDeleted;
        }


        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        /// <summary>
        /// 文章所在分类专栏Id
        /// </summary>
        public int? ClassifyId { get; set; }
        public virtual Classify Classify { get; set; }
        [Column(StringLength = 50)]
        public string Title { get; set; }
        [Column(StringLength = 500)]
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsDeleted { get; set; }

        public byte[] Version { get; set; }

    }

}
