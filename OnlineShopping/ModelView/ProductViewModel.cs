using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ModelView
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public IFormFile ProductImage { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public int CatedoryId { get; set; }
    }
}
