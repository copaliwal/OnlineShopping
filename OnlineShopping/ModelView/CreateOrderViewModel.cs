using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.ModelView
{
    public class CreateOrderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Cart> CartItems { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
    }
}
