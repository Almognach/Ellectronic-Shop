using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electro_Shop.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public String StreetName { get; set; }
        public String CityName { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
    }
}
