using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Models;

namespace TaskApi.Services
{
    public interface ITokenService
    {
        Task<Token> createTokenAsync(User user);
    }
}