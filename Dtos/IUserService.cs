using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskApi.Models;

namespace TaskApi.Dtos
{
    public interface IUserService
    {
        Task<User> getUser();
        Task<IdentityResult> UpdateUserPassword(string oldPassword, string newPassword, string confirmPassword);
    }
}