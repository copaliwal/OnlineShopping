using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using OnlineShopping.Models;

namespace OnlineShopping.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            InsertDefaultValues(builder);
        }

        private void InsertDefaultValues(ModelBuilder builder)
        {
            var context = builder;

            #region [*] Insert Roles

            var roles = new string[] { "Admin", "Customer", "Manager" };
            foreach (var roleName in roles)
            {
                builder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = roleName, NormalizedName = roleName });
            }

            #endregion

            #region [*] Insert Categories
            var categoryNames = new string[] { "Mobile", "Tablet", "Smart Watch", "Laptop", "Speakers", "TV" };
            int i = 1;
            foreach (var categoryName in categoryNames)
            {
                context.Entity<Category>().HasData(new Category { CategoryId = i++, Name = categoryName });
            }
            #endregion

            #region [*] Insert Product
            
            var products = GetProductList();
            int pId = 1;
            foreach (var product in products)
            {
                product.ProductId = pId++;
                context.Entity<Product>().HasData(product);
            }
            
            #endregion
        }
        private List<Product> GetProductList()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product()
            {
                Name = "MI Redmi 8",
                Details = "4 GB RAM | 64 GB ROM | Expandable Upto 512 GB | 15.8 cm (6.22 inch) HD+ Display | 12MP + 2MP | 8MP Front Camera | 5000 mAh Battery | Qualcomm Snapdragon 439 Processor",
                Price = 8499,
                ImageUrl = "/Images/imafhyabpggagngr.jpeg",
                CatedoryId = 1
            });

            products.Add(new Product()
            {
                Name = "MI Redmi Note 7 Pro",
                Details = "4 GB RAM | 64 GB ROM | Expandable Upto 512 GB | 15.8 cm (6.3 inch) HD+ Display",
                Price = 9999,
                ImageUrl = "/Images/imaffm49zzfgw3ty.jpeg",
                CatedoryId = 1
            });

            products.Add(new Product()
            {
                Name = "Apple ipad Mini (2019)",
                Details = "64 GB ROM  | 20.07 cm (7.9 inch) Full HD Display | 8 MP Primary Camera",
                Price = 34900,
                ImageUrl = "/Images/imafe6f78hur4jbh.jpeg",
                CatedoryId = 2
            });

            products.Add(new Product()
            {
                Name = "Microsoft Surface Go",
                Details = "New 10 inch Surface Go, featuring Intel Pentium Gold Processor 4415Y, 4GB RAM",
                Price = 29999,
                ImageUrl = "/Images/imafbgqzhjhmthh2.jpeg",
                CatedoryId = 2
            });

            products.Add(new Product()
            {
                Name = "Lenovo Tab M10",
                Details = "3 GB RAM | 32 GB ROM | Expandable Upto 256 GB | 25.65 cm (10.1 inch) Full HD",
                Price = 16990,
                ImageUrl = "/Images/imafd3nzffhhgsqz.jpeg",
                CatedoryId = 2
            });

            products.Add(new Product()
            {
                Name = "Apple MacBook Air",
                Details = "Intel Core i5 | SSD Capacity - 128 GB | RAM - 8 GB | RAM Type - DDR3",
                Price = 64990,
                ImageUrl = "/Images/Applelaptop12132213.jpg",
                CatedoryId = 4
            });

            return products;
        }
    }
}
