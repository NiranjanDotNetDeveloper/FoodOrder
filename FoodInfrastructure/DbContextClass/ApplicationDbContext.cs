using FoodOrderCoreProject.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodInfrastructure.DbContextClass
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>option):base(option)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(new Category()
            {
                CategoryId = 1,
               CategoryName ="Lunch"
            });
            builder.Entity<Product>().HasData(new Product()
            {
                ProductId=1,
                ProductName="Chicken Tikka",
                Price=80,
                ProductDescription="Nice for lunch",
                CategoryId=1
            });
        }
    }
}
