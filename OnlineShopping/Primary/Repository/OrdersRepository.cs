using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Primary.Repository;
using OnlineShopping.Models;
using OnlineShopping.Primary.Interfaces;
using OnlineShopping.Data;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopping.Primary.Repository
{
    public class OrdersRepository : EFRepository<Order>, IOrdersRepository
    {
        private readonly ApplicationDbContext context;

        public OrdersRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Order> GetByUserId(string userId)
        {
            var orders = context.Orders
                                   .Include("OrderDetails")
                                   .Where(o => o.UserId == userId)
                                   .ToList();
            return orders;
        }

        public void CreateOrder(string userId, string shippingAddress)
        {
            var cartItems = context.Carts
                                       .Include(p => p.Product)
                                       .Where(c => c.UserId == userId)
                                       .ToList();

            Order order = new Order()
            {
                UserId = userId,
                ShippingAddress = shippingAddress,
                Status = "Order Placed",
                TotalAmount = cartItems.Sum(c => c.TotalPrice),
                OrderDate = DateTime.Now
            };

            // step - 1 Add record in Order
            context.Orders.Add(order);

            context.SaveChanges();

            //step - 2 Add cart items in OrderDetails
            foreach (var item in cartItems)
            {
                context.OrderDetails.Add(new OrderDetails()
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = 1,
                    TotalPrice = item.TotalPrice
                });
            }

            //step - 3 remove records from Cart
            context.Carts.RemoveRange(cartItems);

            context.SaveChanges();
        }
    }
}
