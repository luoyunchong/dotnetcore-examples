using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetCore.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignlarDemo.Hubs;

namespace SignlarDemo.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHubContext<MessageHub, IChatClient> _messageHub;

        private readonly IJsonWebTokenService _jsonWebTokenService;
        public UserController(IHubContext<MessageHub, IChatClient> messageHub, IJsonWebTokenService jsonWebTokenService)
        {
            _messageHub = messageHub ?? throw new ArgumentNullException(nameof(messageHub));
            _jsonWebTokenService = jsonWebTokenService;
        }

        [Authorize]
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

        [Authorize]
        public string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
