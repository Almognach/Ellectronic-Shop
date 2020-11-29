using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Electro_Shop.Models;

namespace Electro_Shop.Data
{
    public class ContactUsContext : DbContext
    {
        public ContactUsContext(DbContextOptions<ContactUsContext> options)
            : base(options)
        {
        }

        public DbSet<Electro_Shop.Models.ContactUsSubmit> ContactUs { get; set; }
    }
}
