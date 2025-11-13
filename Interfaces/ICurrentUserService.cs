using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? Email { get; }
        string? Username { get; }
        bool IsAuthenticated { get; }
        string GetUserId();
        string GetEmail();
        string GetUsername();
    }
}