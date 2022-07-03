using Microsoft.AspNetCore.Mvc;

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
