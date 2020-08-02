using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataBase.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostDapperController : ControllerBase
    {
        private readonly AppDb _db;
        public PostDapperController(AppDb db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (_db)
            {
                return new OkObjectResult("");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (_db)
            {
                return new OkObjectResult("");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Post post)
        {
            using (_db)
            {
                await _db.Connection.OpenAsync();
                _db.Connection.Execute(@"INSERT INTO `Post` (`Title`, `Content`) VALUES (@title, @content);", post);
                return new OkObjectResult(post);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Post post)
        {
            using (_db)
            {
                await _db.Connection.OpenAsync();
                string sql = "UPDATE  Post SET Title=@Title,Content=@Content WHERE Id=@Id";
                _db.Connection.Execute(sql, post);
                return new OkObjectResult("");
            }

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            using (_db)
            {
                await _db.Connection.OpenAsync();
                _db.Connection.Execute(@"DELETE FROM  `Post` WHERE id=@id", new { id });
                return new OkObjectResult("");
            }
        }

        [HttpPost("create-list")]
        public async Task<IActionResult> PostList([FromBody]List<Post> posts)
        {
            using (_db)
            {
                await _db.Connection.OpenAsync();
                _db.Connection.Execute(@"INSERT INTO `Post` (`Title`, `Content`) VALUES (@title, @content);", posts);
                return new OkObjectResult(posts);
            }
        }


    }
}