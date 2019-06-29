using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using Microsoft.AspNetCore.Mvc;
using Webapi.FreeSql.Domain;

namespace Webapi.FreeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        // GET api/Blog

        IFreeSql _fsql;
        public BlogController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Blog>> Get()
        {
            List<Blog> blogs = _fsql.Select<Blog>().Where(r => r.Rating > 0.1).OrderBy(r => r.Url).ToList();

            return blogs;
        }

        // GET api/blog/5
        [HttpGet("{id}")]
        public ActionResult<Blog> Get(int id)
        {
            return _fsql.Select<Blog>(id).ToOne();
        }

        // POST api/blog
        [HttpPost]
        public void Post([FromBody] Blog blog)
        {
            _fsql.Insert<Blog>(blog).ExecuteAffrows();
        }

        // PUT api/blog/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/blog/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _fsql.Delete<Blog>(new { BlogId = id }).ExecuteAffrows();
        }
    }
}
