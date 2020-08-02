using System;
using System.Collections.Generic;
using System.Linq;
using OvOv.CsRedis.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Csredis.Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public void Post(string value)
        {
            RedisHelper.Set("test1", value, 60);
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return RedisHelper.Get("test1");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public UserInfo GetUser(int id)
        {
            return GetUserInfos().FirstOrDefault(r => r.Id == id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            RedisHelper.Del("test1");
        }

        /// <summary>
        ///根据id获取到的数据缓存3600s：一般的缓存代码，如不封装还挺繁琐的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUserCache")]
        public UserInfo GetUserCache(int id)
        {
            string userRedisKey = "user:info:" + id;
            string value = RedisHelper.Get(userRedisKey);
            UserInfo userInfo;
            if (value != null)
            {
                userInfo = RedisHelper.Get<UserInfo>(userRedisKey);
            }
            else
            {
                userInfo = GetUserInfos().FirstOrDefault(r => r.Id == id);

                if (userInfo != null)
                {
                    RedisHelper.Set(userRedisKey, userInfo, 3600);
                }
            }
            return userInfo;
        }

        /// <summary>
        /// 缓存壳方式缓存数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUserCacheShell")]
        public UserInfo GetUserCacheShell(int id)
        {
            string userRedisKey = "user:info:" + id;

            return RedisHelper.CacheShell(userRedisKey, 3600, () => GetUserInfos().FirstOrDefault(r => r.Id == id));
        }


        /// <summary>
        ///  使用数据模拟数据库获取的信息
        /// </summary>
        /// <returns></returns>
        private List<UserInfo> GetUserInfos()
        {
            return new List<UserInfo>()
            {
                new UserInfo(1,"test-name-1",1),
                new UserInfo(2,"test-name-2",2),
                new UserInfo(3,"test-name-3",3),
                new UserInfo(4,"test-name-4",4),
                new UserInfo(5,"test-name-5",5),
                new UserInfo(6,"test-name-6",6),
            };
        }

    }
}
