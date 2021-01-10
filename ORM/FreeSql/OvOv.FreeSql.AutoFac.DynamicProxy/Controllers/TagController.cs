    using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OvOv.Core.Domain;
using OvOv.Core.Web;
using OvOv.FreeSql.AutoFac.DynamicProxy.Services;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private readonly TagService _tagService;
        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public Task<PagedResultDto<Tag>> GetAsync([FromQuery] PageDto pageDto)
        {
            return _tagService.GetAsync(pageDto);
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] Tag tag)
        {
            await _tagService.UpdateAsync(tag);
        }

        [HttpPost("CreateTransactionalAsync")]
        public async Task CreateTransactionalAsync([FromBody] Tag tag)
        {
            await _tagService.CreateTransactionalAsync(tag);
        }

        [HttpPost("CreateAsync")]
        public async Task CreateAsync([FromBody] Tag tag)
        {
            await _tagService.CreateAsync(tag);
        }
    }


}