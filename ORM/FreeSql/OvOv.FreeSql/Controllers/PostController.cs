using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Mvc;
using OvOv.Core.Domain;
using OvOv.Core.Models.Posts;
using OvOv.Core.Web;

namespace OvOv.FreeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        // GET: api/Post
        private readonly IFreeSql _fsql;
        private readonly IMapper _mapper;
        public PostController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        /// <summary>
        /// 根据博客id、分页条件查询评论信息
        /// </summary>
        /// <param name="searchPostDto"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedResultDto<Post> Get([FromQuery] SearchPostDto searchPostDto)
        {
            ISelect<Post> selectPost = _fsql
                .Select<Post>()
                .Where(r => r.BlogId == searchPostDto.BlogId);

            List<Post> posts = selectPost.OrderByDescending(r => r.ReplyTime)
                .Page(searchPostDto.PageNumber, searchPostDto.PageSize)
                .ToList();

            long total = selectPost.Count();

            return new PagedResultDto<Post>(total, posts);
        }

        // GET: api/Post/5
        [HttpGet("{id}", Name = "Get")]
        public Post Get(int id)
        {
            return _fsql.Select<Post>().Where(a => a.PostId == id).ToOne();
        }

        // POST: api/Post
        [HttpPost]
        public void Post([FromBody] CreatePostDto createPostDto)
        {
            Post post = _mapper.Map<Post>(createPostDto);
            post.ReplyTime = DateTime.Now;
            _fsql.Insert(post).ExecuteAffrows();
        }


        // DELETE: api/Post/
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _fsql.Delete<Post>(new Post { PostId = id }).ExecuteAffrowsAsync();
        }
    }
}
