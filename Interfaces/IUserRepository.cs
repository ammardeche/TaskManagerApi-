using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskApi.Models;

namespace TaskApi.Interfaces
{
    public interface IUserRepository
    {

        Task<User> GetUser(string userId);
        Task<IdentityResult> UpdatePassword(User user, string currentPassword, string newPassword);

    }
}