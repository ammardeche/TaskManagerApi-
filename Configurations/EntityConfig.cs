using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskApi.Configurations
{
  public static class EntityConfig
  {


    public static void AddEntityConfig(ModelBuilder builder)
    {

      // // Entities Keys keys 
      // builder.Entity<Customer>()
      //   .HasKey(p => p.Id);
      // builder.Entity<Delivery>()
      //     .HasKey(d => d.Id);


      // // relationship between Entities 

      // builder.Entity<Customer>()
      //         .HasOne(c => c.User)
      //         .WithOne(u => u.Customer)
      //         .HasForeignKey<Customer>(c => c.UserId)
      //         .OnDelete(DeleteBehavior.Cascade);

      // builder.Entity<Delivery>()
      //     .HasOne(c => c.User)
      //     .WithOne(c => c.Delivery)
      //     .HasForeignKey<Delivery>(u => u.UserId)
      //     .OnDelete(DeleteBehavior.Cascade);

    }

  }
}