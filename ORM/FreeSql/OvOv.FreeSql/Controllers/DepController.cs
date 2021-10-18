using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OvOv.Core.Web;

namespace OvOv.FreeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepController : ControllerBase
    {
        private IFreeSql _fsql;
        public DepController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        [HttpPost]
        public long Post([FromBody] DepInfo depDto)
        {
            return _fsql.Insert(depDto).ExecuteIdentity();
        }
    }
}
