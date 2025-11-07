using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskApi.Interfaces;
using TaskApi.Models;

namespace TaskApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthService(ITokenService tokenService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Token> LoginAsync(string Email, string Password)
        {
            // check if the user exists
            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                throw new KeyNotFoundException("user does not exist");
            }

            // check if the password is correct
            var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, Password, false);
            if (!checkPassword.Succeeded)
            {
                throw new UnauthorizedAccessException("invalid Password");
            }
            var token = await _tokenService.createTokenAsync(user);
            return token;

        }

        public async Task<Token> RegisterAsync(string FullName, string Email, string Password, string ConfirmPassword)
        {
            // check if the user already exists
            var userExists = await UserExistsAsync(Email);
            if (userExists)
            {
                throw new KeyNotFoundException("user already exist");
            }
            // check if password and confirm password match
            if (Password != ConfirmPassword)
            {
                throw new ArgumentException("password and confirm password do not match");
            }
            var user = new User
            {
                UserName = Email,
                Email = Email,
                FullName = FullName,
            };
            var result = await _userManager.CreateAsync(user, Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(u => u.Description));
                throw new InvalidOperationException($"user creation failed: {errors}");
            }

            var createRole = await _userManager.AddToRoleAsync(user, "User");
            if (!createRole.Succeeded)
            {
                var errors = string.Join(",", createRole.Errors.Select(u => u.Description));
                throw new InvalidOperationException($"role assignment failed: {errors}");
            }
            var token = await _tokenService.createTokenAsync(user);
            return token;
        }

        public async Task<bool> UserExistsAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(email: Email);
            return user != null;

        }
    }
}