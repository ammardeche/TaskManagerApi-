using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskApi.Dtos;
using TaskApi.Interfaces;
using TaskApi.Models;
using TaskApi.Repositories;

namespace TaskApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, UserManager<User> userManager, ICurrentUserService CurrentUserService, ITokenService tokenService)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _currentUserService = CurrentUserService;
            _tokenService = tokenService;
        }
        public async Task<User> getUser()
        {
            // get the current user id 

            var userId = _currentUserService.GetUserId();

            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("user doesn't found");
            }

            return user;
        }

        public async Task<IdentityResult> UpdateUserPassword(string oldPassword, string newPassword, string confirmPassword)
        {
            // get the current user id 
            var userId = _currentUserService.GetUserId();
            // check if the user already exist 
            var user = await _userRepository.GetUser(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User Doesn't found");
            }

            if (newPassword != confirmPassword)
            {
                throw new ArgumentException("The new password doesn't match the confirm password");
            }

            // ✅ ADD THIS VALIDATION:
            var isOldPasswordCorrect = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (!isOldPasswordCorrect)
            {

                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidCurrentPassword",
                    Description = "The current password is incorrect"
                });
            }

            return await _userRepository.UpdatePassword(user, oldPassword, newPassword);


        }
    }
}