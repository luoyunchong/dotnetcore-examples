using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Db;
using DataBase.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDb _db;
        public PostController(AppDb db)
        {
            _db = db;
        }

        // GET api/post
        [HttpGet]
        public async Task<IActionResult> LatestPostsAsync()
        {
            using (_db)
            {
                await _db.Connection.OpenAsync();
                var query = new PostQuery(_db);
                var result = await query.LatestPostsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/post/5
        [HttpGet("{id}")]
        public async Task<IActionResult> FindOneAsync(int id)
        {
            using (_db)
            {
                await _db.Connection.OpenAsync();
                var query = new PostQuery(_db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody]Post post)
        {
            using (_db)
            {
                await _db.Connection.OpenAsync();
                post.Db = _db;
                await post.InsertAsync();
                return new OkObjectResult(post);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]Post post)
        {
            using (_db)
            {
                await _db.Connection.OpenAsync();
                post.Db = _db;
                await post.UpdateAsync();
                return new OkObjectResult(post);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            using (_db)
            {
                await _db.Connection.OpenAsync();
                await new Post() { Id = id }.DeleteAsync();
                return new OkObjectResult(id);
            }
        }
    }
}