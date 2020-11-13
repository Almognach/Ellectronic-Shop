using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Electro_Shop.Models;

namespace Electro_Shop.Data
{
    public class ShoppingCartContext : DbContext
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
            : base(options)
        {
        }

        public DbSet<Electro_Shop.Models.ShoppingCart> ShoppingCartLines { get; set; }
    }
}
