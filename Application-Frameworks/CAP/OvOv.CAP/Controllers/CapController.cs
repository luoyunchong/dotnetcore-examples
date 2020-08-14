using System;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

namespace OvOv.CAP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CapController : ControllerBase
    {
        [HttpGet("send")]
        public IActionResult SendMessage([FromServices] ICapPublisher capBus)
        {
            capBus.Publish("test.show.time", DateTime.Now);

            return Ok();
        }

        [NonAction]
        [CapSubscribe("test.show.time")]
        public void ReceiveMessage(DateTime time)
        {
            Console.WriteLine("message time is:" + time);
        }
    }
}
