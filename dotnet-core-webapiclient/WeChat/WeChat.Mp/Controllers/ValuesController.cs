using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiClient;

namespace WeChat.Mp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<ResultMsg>> GetAsync(string jscode="")
        {
            var api = HttpApi.Create<IMyWebApi>();
            var response = await api.Jscode2Session("", "", jscode);
            return response;
        }
    }
}
