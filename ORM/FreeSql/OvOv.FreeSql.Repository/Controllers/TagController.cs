using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Mvc;
using OvOv.Core.Domain;
using OvOv.Core.Models.Blogs;
using OvOv.FreeSql.Repository.Repositories;
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