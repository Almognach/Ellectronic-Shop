using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Electro_Shop.Areas.Identity.Data;

namespace Electro_Shop.Models
{
    public class PastOrdersView
    {
        [Key]
        public string UserID { get; set; }
        [Required]
        public List<OrderView> OrdersView { get; set; }
    }
    public class OrderView
    {
        [Key]
        public string UserID { get; set; }
        [Required]
        public Order Order { get; set; }
        [Required]
        public List<OrderLine> OrderLines { get; set; }
        [Required]
        public double PricePaid { get; set; }
    }

}
