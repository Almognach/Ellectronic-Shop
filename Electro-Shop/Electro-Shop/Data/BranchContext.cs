using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Electro_Shop.Models;

namespace Electro_Shop.Data
{
    public class BranchContext : DbContext
    {
        public BranchContext (DbContextOptions<BranchContext> options)
            : base(options)
        {
        }

        public DbSet<Electro_Shop.Models.Branch> Branch { get; set; }
    }
}
