using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TaskApi.Data
{
    public static class SeedRoleData
    {
        public static void SeedRoles(ModelBuilder builder)
        {

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "86bfb5cb-8578-48a8-a8b3-4e5b4a90c004", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "aeecdb37-c41a-449b-b599-ebc9a8702f35", Name = "User", NormalizedName = "USER" }
            );
        }
    }
}