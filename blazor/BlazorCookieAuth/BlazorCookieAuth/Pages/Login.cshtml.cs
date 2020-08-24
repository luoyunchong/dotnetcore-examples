using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorCookieAuth.Server.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public string ReturnUrl { get; set; }

        public async Task<IActionResult>
            OnGetAsync(string paramUsername, string paramPassword)
        {
            string returnUrl = Url.Content("~/");

            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }

            // *** !!! This is where you would validate the user !!! ***
            // In this example we just log the user in
            // (Always log the user in for this demo)

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, paramUsername),
               new Claim("FullName", "balabal"),
               new Claim(ClaimTypes.Role, "VIP客户"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //自动延长用户的登录时间（其实就是延长cookie在客户端计算机硬盘上的保留时间）
                AllowRefresh = true,
                //验证会话是否持续存在
                IsPersistent = true,
                //身份验证票证到期的时间
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                //发出身份验证票证的时间。
                IssuedUtc =new DateTimeOffset(),
                //用作http的完整路径或绝对URI
                RedirectUri = this.Request.Host.Value
            };

            try
            {
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
             

            return LocalRedirect(returnUrl);
        }
    }
}