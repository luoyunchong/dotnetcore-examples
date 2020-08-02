using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace StackExchange.Redis.Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        IDistributedCache _Cache;

        public ValuesController(IDistributedCache cache)
        {
            _Cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var image=_Cache.Get("ImagePropertyKey");
            
            //获取缓存
            if (image!=null&&image.Length>0)
            {
                var key = Encoding.UTF8.GetString(image);

                byte[] val = null;
                val = Encoding.UTF8.GetBytes("ImageProertyValue");
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                //设置绝对过期时间 两种写法
                options.AbsoluteExpiration = DateTime.Now.AddMinutes(30);
                // options.SetAbsoluteExpiration(DateTime.Now.AddMinutes(30));
                //设置滑动过期时间 两种写法
                options.SlidingExpiration = TimeSpan.FromSeconds(30);
                //options.SetSlidingExpiration(TimeSpan.FromSeconds(30));
                //添加缓存
                _Cache.Set(key, val, options);
                //刷新缓存
                _Cache.Refresh(key);


            }
            else
            {
                //移除缓存
                _Cache.Remove("ImagePropertyKey");
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
