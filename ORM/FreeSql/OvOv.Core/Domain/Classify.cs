using FreeSql.DataAnnotations;
using System.Collections.Generic;

namespace OvOv.Core.Domain
{
    /// <summary>
    /// 分类
    /// </summary>
    public class Classify
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortCode { get; set; }
        /// <summary>
        /// 分类专栏名称
        /// </summary>
        [Column(DbType = "varchar(50)")]
        public string ClassifyName { get; set; }

        public virtual List<Blog> Articles { get; set; }

    }
}
