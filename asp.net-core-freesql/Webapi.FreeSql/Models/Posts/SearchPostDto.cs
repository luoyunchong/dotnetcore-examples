using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapi.FreeSql.Web;

namespace Webapi.FreeSql.Models.Posts
{
    public class SearchPostDto:PageDto
    {
        public int BlogId { get; set; }
    }
}
