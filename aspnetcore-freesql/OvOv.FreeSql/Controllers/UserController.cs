using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OvOv.Core.Domain;
using OvOv.Core.Models;
using OvOv.Core.Web;

namespace OvOv.FreeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IFreeSql _fsql;
        private readonly IMapper _mapper;
        public UserController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        [HttpGet]
        public PagedResultDto<User> Get([FromQuery] PageDto pageDto)
        {
            ISelect<User> selectPost = _fsql
                .Select<User>();

            List<User> lists = selectPost
                .Page(pageDto.PageNumber, pageDto.PageSize)
                .ToList();

            long total = selectPost.Count();

            return new PagedResultDto<User>(total, lists);
        }


        [HttpPost]
        public void Post([FromBody] CreateUserDto createUser)
        {
            User user = new User()
            {
                Extra = JsonConvert.SerializeObject(createUser.ExtraJson),
                Name = createUser.Name
            };
            _fsql.Insert(user).ExecuteAffrows();
        }
    }
}
