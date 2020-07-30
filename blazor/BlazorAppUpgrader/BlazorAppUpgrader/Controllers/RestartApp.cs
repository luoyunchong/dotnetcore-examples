using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SharedClassLibrary;

namespace BlazorAppUpgrader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestartAppController : Controller
    {
        private IHostApplicationLifetime ApplicationLifetime { get; set; }

        public RestartAppController(IHostApplicationLifetime appLifetime)
        {
            ApplicationLifetime = appLifetime;
        }

        // /api/RestartApp/ShutdownSite
        [HttpGet("[action]")]
        public RestartAppModel ShutdownSite()
        {
            // In a real application only allow an Administrator
            // to call this method
            ApplicationLifetime.StopApplication();
            return new RestartAppModel() { Messsage = "Done", IsSuccess = true };
        }
    }
}