using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electro_Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public uint SalesCounter { get; set; }
        public uint InStock { get; set; }
        public Uri PathToImage { get; set; }

    }
}
