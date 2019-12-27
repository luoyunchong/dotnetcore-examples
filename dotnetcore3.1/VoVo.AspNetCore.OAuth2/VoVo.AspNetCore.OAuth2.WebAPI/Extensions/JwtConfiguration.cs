using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace VoVo.AspNetCore.OAuth2.WebAPI.Extensions
{
    public static class JwtConfiguration
    {
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(opts =>
                {
                    opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddCookie(options =>
            {
                options.LoginPath = "/signin";
                options.LogoutPath = "/signout";
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Audience = configuration["Authentication:JwtBearer:Audience"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here
                    //ClockSkew = TimeSpan.Zero
                };
            }).AddGitHub(options =>
            {
                options.ClientId = configuration["Authentication:GitHub:ClientId"];
                options.ClientSecret = configuration["Authentication:GitHub:ClientSecret"];
                //options.CallbackPath = new PathString("~/signin-github");//与GitHub上的回调地址相同，默认即是/signin-github
                options.Scope.Add("user:email");
                //authenticateResult.Principal.FindFirst(LinConsts.Claims.AvatarUrl)?.Value;  得到GitHub头像
                options.ClaimActions.MapJsonKey(LinConsts.Claims.AvatarUrl, "avatar_url");
                options.ClaimActions.MapJsonKey(LinConsts.Claims.BIO, "bio");
                options.ClaimActions.MapJsonKey(LinConsts.Claims.BlogAddress, "blog");
            });
        }
    }
}
