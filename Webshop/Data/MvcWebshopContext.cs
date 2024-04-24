using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcWebshop.Data
{
    public class MvcWebshopContext : DbContext
    {
        public MvcWebshopContext (DbContextOptions<MvcWebshopContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<ShoppingCart> ShoppingCart { get; set; } = default!;
    }
}
