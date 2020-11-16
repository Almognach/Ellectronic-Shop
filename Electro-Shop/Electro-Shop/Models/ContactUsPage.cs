using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electro_Shop.Models
{
    public class ContactUs
    {
        public List<Branch> Branches { get; set; }
        public ContactUsSubmit Submit { get; set; }
    }
}
