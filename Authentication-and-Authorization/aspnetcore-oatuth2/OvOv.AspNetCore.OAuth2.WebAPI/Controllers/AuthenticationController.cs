﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using VoVo.AspNetCore.OAuth2.WebAPI.Extensions;

namespace VoVo.AspNetCore.OAuth2.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private const string LoginProviderKey = "LoginProvider";
        private readonly IConfiguration _configuration;

        public AuthenticationController(ILogger<AuthenticationController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        /// <summary>
        /// 授权成功后自动回调的地址
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="redirectUrl">授权成功后的跳转地址</param>
        /// <returns></returns>
        [HttpGet("~/signin-callback")]
        public async Task<IActionResult> Home(string provider = null, string redirectUrl = "")
        {
            var authenticateResult = await _contextAccessor.HttpContext.AuthenticateAsync(provider);
            if (!authenticateResult.Succeeded) return Redirect(redirectUrl);
            var openIdClaim = authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier);
            if (openIdClaim == null || string.IsNullOrWhiteSpace(openIdClaim.Value))
                return Redirect(redirectUrl);

            //TODO 记录授权成功后的信息 

            string email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
            string name = authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value;
            string gitHubName = authenticateResult.Principal.FindFirst(GitHubAuthenticationConstants.Claims.Name)?.Value;
            string gitHubUrl = authenticateResult.Principal.FindFirst(GitHubAuthenticationConstants.Claims.Url)?.Value;
            //startup 中 AddGitHub配置项  options.ClaimActions.MapJsonKey(LinConsts.Claims.AvatarUrl, "avatar_url");
            string avatarUrl = authenticateResult.Principal.FindFirst(LinConsts.Claims.AvatarUrl)?.Value;

            string token = this.CreateToken(authenticateResult.Principal);

            return Redirect($"{redirectUrl}?token={token}");
        }

        /// <summary>
        /// https://localhost:5001/signin?provider=GitHub&redirectUrl=http://localhost:8080/login-result
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        [HttpGet("~/signin")]
        public async Task<IActionResult> SignIn(string provider, string redirectUrl)
        {
            // Note: the "provider" parameter corresponds to the external
            // authentication provider choosen by the user agent.
            if (string.IsNullOrWhiteSpace(provider))
            {
                return BadRequest();
            }

            if (!await HttpContext.IsProviderSupportedAsync(provider))
            {
                return BadRequest();
            }

            var request = _contextAccessor.HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}-callback?provider={provider}&redirectUrl={redirectUrl}";
            var properties = new AuthenticationProperties { RedirectUri = url };
            properties.Items[LoginProviderKey] = provider;
            return Challenge(properties, provider);
        }

        //https://localhost:5001/signout
        [HttpGet("~/signout"), HttpPost("~/signout")]
        public IActionResult SignOut()
        {
            // Instruct the cookies middleware to delete the local cookie created
            // when the user agent is redirected from the external identity provider
            // after a successful authentication flow (e.g Google or Facebook).
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// axios https://localhost:5001/OpenId?provider=GitHub 得到null，浏览器直接打开这个地址能得到github的id 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        [HttpGet("~/OpenId")]
        public async Task<string> OpenId(string provider = null)
        {
            var authenticateResult = await _contextAccessor.HttpContext.AuthenticateAsync(provider);
            if (!authenticateResult.Succeeded) return null;
            var openIdClaim = authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier);
            return openIdClaim?.Value;

        }

        /// <summary>
        /// 前后端分离，前台请求头传递 Authorization:Bearer+空格+Token
        /// </summary>
        /// <returns></returns>
        [HttpGet("~/GetOpenIdByToken")]
        public string GetOpenIdByToken()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private string CreateToken(ClaimsPrincipal claimsPrincipal)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Authentication:JwtBearer:SecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Authentication:JwtBearer:Issuer"],
                _configuration["Authentication:JwtBearer:Audience"],
                claimsPrincipal.Claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return handler.WriteToken(token);
        }
    }
}
