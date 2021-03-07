using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Models;

namespace OnlineShopping.Primary.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        int GetCount(string userId);
        IEnumerable<Cart> GetByUserId(string userId);
    }
}
