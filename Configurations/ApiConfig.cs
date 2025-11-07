using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi.Configurations
{
    public static class ApiConfig
    {
        public static WebApplicationBuilder AddApiConfig(this WebApplicationBuilder builder)
        {
            _ = builder.Services.AddControllers();

            return builder;
        }
    }
}