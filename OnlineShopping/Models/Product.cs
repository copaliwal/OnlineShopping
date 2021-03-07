using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public int CatedoryId { get; set; }

        [ForeignKey("CatedoryId")]
        public virtual Category Category { get; set; }
    }
}
