using FreeSql.DataAnnotations;
using System;

namespace Navigation
{
    public class NavigationLink
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string LogUrl { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
