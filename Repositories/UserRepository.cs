using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskApi.Interfaces;
using TaskApi.Models;

namespace TaskApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<User> GetUser(string userId) => await _userManager.FindByIdAsync(userId);

        public async Task<IdentityResult> UpdatePassword(User user, string currentPassword, string newPassword) =>
        await _userManager.ChangePasswordAsync(user, currentPassword: currentPassword, newPassword);



    }
}