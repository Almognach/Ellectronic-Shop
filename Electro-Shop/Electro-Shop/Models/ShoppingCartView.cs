using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Electro_Shop.Areas.Identity.Data;

namespace Electro_Shop.Models
{
    public class Product_Quantity
    {
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double FinalPrice { get; set; }
    }

    public class ShoppingCartView
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public List<Product_Quantity> Product_Quantity { get; set; }
        [Required]
        public double FinalPrice { get; set; }
    }
}
