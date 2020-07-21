using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DataAnnotations;

namespace OvOv.Core.Domain
{
    public class User : ISoftDelete
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        [Column(DbType = "json")]
        public string Extra { get; set; }

    }

    public class ExtraJson
    {
        public string Salt { get; set; }

        public string Password { get; set; }
    }
}
