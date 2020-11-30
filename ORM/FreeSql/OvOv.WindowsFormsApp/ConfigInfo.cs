
using FreeSql.DataAnnotations;

namespace WindowsFormsApp1
{
    public class ConfigInfo
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
    }
}