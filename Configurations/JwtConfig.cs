using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskApi.Models;

namespace TaskApi.Configurations
{
    public static class JwtConfig
    {

        public static WebApplicationBuilder AddJwtConfig(this WebApplicationBuilder builder)
        {
            IConfigurationSection jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
            _ = builder.Services.Configure<JwtSettings>(jwtSettingsSection);

            JwtSettings? jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            byte[] key = Encoding.ASCII.GetBytes(jwtSettings!.Secret);

            _ = builder.Services.AddAuthentication(x =>
     {
         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
     }).AddJwtBearer(x =>
     {
         x.RequireHttpsMetadata = false;
         x.SaveToken = true;
         x.TokenValidationParameters = new TokenValidationParameters
         {
             IssuerSigningKey = new SymmetricSecurityKey(key),
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidAudience = jwtSettings.Audience,
             ValidIssuer = jwtSettings.Issuer
         };
     });
            return builder;
        }

    }
}