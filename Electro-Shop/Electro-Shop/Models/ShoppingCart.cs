using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Electro_Shop.Areas.Identity.Data;

namespace Electro_Shop.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
