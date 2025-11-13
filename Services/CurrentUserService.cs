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
            var userId = _httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // ✅ Temporary: Return test user if no authenticated user
            if (string.IsNullOrEmpty(userId))
            {
                // Use a real user ID from your database
                return "9f33f528-1921-4413-88d1-f02b8d96c3eb"; //  Replace with actual user ID from your Users table
            }

            return userId;
        }

        // create fir the first the user all of in the second tem of conditions all of them need share about one
        public string GetUsername()
        {
            return Username ?? throw new UnauthorizedAccessException("User Not Found");
        }
    }
}