using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "Order Id")]
        public int OrderId { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
