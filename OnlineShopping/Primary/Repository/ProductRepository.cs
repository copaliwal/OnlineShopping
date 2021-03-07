using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Primary.Interfaces;
using OnlineShopping.Primary.Repository;
using OnlineShopping.Models;
using OnlineShopping.Data;

namespace OnlineShopping.Primary.Repository
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context) :base (context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetByCategoryId(int id)
        {
            return context.Products.Where(p => p.CatedoryId == id).ToList();
        }
    }
}
