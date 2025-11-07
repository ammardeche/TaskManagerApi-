using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Models;

namespace TaskApi.Interfaces
{
    public interface IAuthService
    {
        Task<Token> RegisterAsync(string FullName, string Email, string Password, string ConfirmPassword);
        Task<bool> UserExistsAsync(string Email);
        Task<Token> LoginAsync(string Email, string Password);
    }
}