using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Electro_Shop.Areas.Identity.Data;

namespace Electro_Shop.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string TimeStamp { get; set; }
    }
}
