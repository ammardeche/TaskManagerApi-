using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using TaskApi.Interfaces;
using TaskApi.Repositories;
using TaskApi.Services;

namespace TaskApi.Configurations
{
    public static class AppServicesConfig
    {

        public static IServiceCollection AddAppServicesConfig(this IServiceCollection services)
        {
            _ = services.AddOpenApi()
             .AddScoped<ITokenService, TokenService>()
             .AddScoped<IAuthService, AuthService>()
             .AddHttpContextAccessor()
             .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddScoped<ITaskItemRepository, TaskItemRepository>();
            // return services;

            return services;
        }

    }

}