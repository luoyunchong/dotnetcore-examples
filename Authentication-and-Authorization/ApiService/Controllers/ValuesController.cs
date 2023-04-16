using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiService.Controllers
{
    //client_id:client.api.service
    //client_secret:clientsecret
    //grant_type:password
    //username:edison@hotmail.com
    //password:edisonpassword
    /// <summary>
    /// 这是注释
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] UserLikeDto userLikeDto)
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

    public class UserLikeDto
    {
        public SubjectType SubjectType { get; set; }
        public long SubjectId { get; set; }
    }

    public enum SubjectType
    {
        A = 1,
        B = 2
    }
}
