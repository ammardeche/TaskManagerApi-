using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;
using TaskApi.Services;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("/api/external")]
    public class ExternalAuthController : Controller
    {

        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        public ExternalAuthController(ITokenService tokenService, UserManager<User> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }
        [HttpGet("signin-google")]
        public async Task<IActionResult> SignInWithGoogle()
        {

            var redirectUrl = Url.Action("GoogleResponse");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);

        }
        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal?.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            var user = await _userManager.FindByEmailAsync(email!);


            if (user == null)
            {
                user = new User
                {
                    UserName = email,
                    Email = email,
                    FullName = name
                };
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "User");
            }

            var token = await _tokenService.createTokenAsync(user);
            return Ok(token);
        }

    }
}
