using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electro_Shop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int SalesCounter { get; set; }
        public int InStock { get; set; }
        public Uri PathToImage { get; set; }
        public Supplier Supplier { get; set; }
    }
}
