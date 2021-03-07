using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Models;

namespace OnlineShopping.Primary.Interfaces
{
    public interface IOrdersRepository : IRepository<Order>
    {
        IEnumerable<Order> GetByUserId(string userId);

        void CreateOrder(string userId, string shippingAddress);
    }
}
