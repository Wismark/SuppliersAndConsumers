using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SuppliersAndConsumers.Db.Entities;

namespace SuppliersAndConsumers.Db
{
    class EfDbContext:DbContext
    {
        public DbSet<City> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
