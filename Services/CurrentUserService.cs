using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskApi.Interfaces;

namespace TaskApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

        public string? Username => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

        public bool IsAuthenticated => UserId != null;

        public string GetEmail()
        {
            return Email ?? throw new UnauthorizedAccessException("User Email not Found");
        }

        public string GetUserId()
        {
            return UserId ?? throw new UnauthorizedAccessException("User id not found");
        }

        public string GetUsername()
        {
            return Username ?? throw new UnauthorizedAccessException("User Not Found");
        }
    }
}