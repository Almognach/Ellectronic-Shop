using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electro_Shop.Models
{
    public class BestSeller
    {
        
        public Category Category { get; set; }
        public List<Product> Products { get; set; }

    }
}
