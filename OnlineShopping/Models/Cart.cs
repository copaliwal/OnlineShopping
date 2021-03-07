using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OnlineShopping.Data;

namespace OnlineShopping.Models
{
    public class Cart
    {
        public Cart()
        {
            this.DateAdded = DateTime.Now;
        }
        [Key]
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
