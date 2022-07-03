using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OvOv.Core.Domain;
using OvOv.FreeSql.Repository.Services;

namespace OvOv.FreeSql.Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] Tag tag)
        {
           await _tagService.UpdateAsync(tag);
        }
        
        [HttpPost]
        public async Task CreateAsync([FromBody] Tag tag)
        {
          await _tagService.CreateAsync(tag);
        }
    }


}