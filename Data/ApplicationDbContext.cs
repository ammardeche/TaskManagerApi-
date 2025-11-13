using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskApi.Models;


namespace TaskApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Category> Categories { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoleData.SeedRoles(builder);
            SeedCategoryData.SeedCategory(builder);
            // relationship between User and TaskItem
            builder.Entity<User>()
            .HasMany(u => u.TaskItems)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            // relationship between User and Category
            builder.Entity<User>()
            .HasMany(u => u.Categories)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            // relationship between Category and TaskItem
            builder.Entity<Category>()
                .HasMany(c => c.TaskItems)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId)
                // Prevent cascade delete here to avoid multiple cascade paths when User -> TaskItems and User -> Categories -> TaskItems
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}