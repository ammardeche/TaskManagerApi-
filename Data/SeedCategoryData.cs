using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApi.Models;

namespace TaskApi.Data
{
    public static class SeedCategoryData
    {
        public static void SeedCategory(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
       new Category { Id = "1", Name = "Personal", IsDefault = true, UserId = null },
       new Category { Id = "2", Name = "Work", IsDefault = true, UserId = null },
       new Category { Id = "3", Name = "Study", IsDefault = true, UserId = null },
       new Category { Id = "4", Name = "Health", IsDefault = true, UserId = null },
       new Category { Id = "5", Name = "Shopping", IsDefault = true, UserId = null },
       new Category { Id = "6", Name = "Other", IsDefault = true, UserId = null }
   );
        }
    }
}