using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Electro_Shop.Models;

namespace Electro_Shop.Data
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options)
            : base(options)
        {
        }

        public DbSet<Electro_Shop.Models.Order> Orders { get; set; }

        public DbSet<Electro_Shop.Models.OrderLine> OrderLines { get; set; }
    }
}
