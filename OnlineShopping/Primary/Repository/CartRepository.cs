using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Data;
using OnlineShopping.Primary.Interfaces;
using OnlineShopping.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopping.Primary.Repository
{
    public class CartRepository : EFRepository<Cart>, ICartRepository
    {
        #region Constructor
        private readonly ApplicationDbContext context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        #endregion

        public int GetCount(string userId)
        {
            int count = 0;
            count = context.Carts.Count(c => c.UserId == userId);

            return count;
        }

        public IEnumerable<Cart> GetByUserId(string userId)
        {
            var cartItems = context.Carts
                                       .Include(p => p.Product)
                                       .Where(c => c.UserId == userId)
                                       .ToList();

            return cartItems;
        }
    }
}
