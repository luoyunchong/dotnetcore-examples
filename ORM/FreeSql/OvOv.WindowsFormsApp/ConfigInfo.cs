
using System;

using FreeSql.DataAnnotations;

namespace WindowsFormsApp1
{
    public class ConfigInfo
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public int Count { get; set; }
        [Column(DbType ="TimeStamp")]
        public DateTime CreateTime { get; set; }
    }
}