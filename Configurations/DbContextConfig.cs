using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApi.Data;

namespace TaskApi.Configurations
{
    public static class DbContextConfig
    {

        public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
        {
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string DefaultConnection not found");
            _ = builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

            return builder;
        }

    }
}