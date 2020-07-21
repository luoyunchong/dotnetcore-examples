using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetCore.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignlarDemo.Models;

namespace SignlarDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<MessageHub, IChatClient> _messageHub;

        private readonly IJsonWebTokenService _jsonWebTokenService;
        public HomeController(IHubContext<MessageHub, IChatClient> messageHub, IJsonWebTokenService jsonWebTokenService)
        {
            _messageHub = messageHub ?? throw new ArgumentNullException(nameof(messageHub));
            _jsonWebTokenService = jsonWebTokenService;
        }

        public async Task<IActionResult> Index()
        {
            await _messageHub.Clients.All.Notify($"Home page loaded at: {DateTime.Now}");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<string> SendMessage(string toUser, string message)
        {
            await _messageHub.Clients.User(toUser).ReceiveMessage(GetUserId(), message);
            return "ok";
        }

        [HttpGet]
        public string Login(string loginName)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim (ClaimTypes.NameIdentifier,loginName)
            };
            string token = _jsonWebTokenService.Encode(claims);

            return token;
        }

        public string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
