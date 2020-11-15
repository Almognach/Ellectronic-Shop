using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Electro_Shop.Models;

namespace Electro_Shop.Data
{
    public class SupplierContext : DbContext
    {
        public SupplierContext (DbContextOptions<SupplierContext> options)
            : base(options)
        {
        }

        public DbSet<Electro_Shop.Models.Supplier> Supplier { get; set; }
    }
}
