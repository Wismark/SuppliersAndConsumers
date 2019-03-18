using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SuppliersAndConsumers.Db.Entities;

namespace SuppliersAndConsumers.Db
{
    public class EfDbContext : DbContext
    {
        public EfDbContext()
            : base("DbConnection")
        { }
     

        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(s => s.Suppliers)
                .WithMany(c => c.Products)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProductRefId");
                    cs.MapRightKey("SupplierRefId");
                    cs.ToTable("SupplierProduct");
                });

            modelBuilder.Entity<Product>()
                .HasMany(s => s.Customers)
                .WithMany(c => c.WishList)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProductRefId");
                    cs.MapRightKey("CustomerRefId");
                    cs.ToTable("CustomerProduct");
                });
        }

    }

    public class EfDbContextInitializer : DropCreateDatabaseAlways<EfDbContext>
    {
        protected override void Seed(EfDbContext db)
        {
            var city = new City() {CityName = "Moscow"};
            var city2 = new City() { CityName = "Voronezh" };
            var city3 = new City() { CityName = "St. Petersburg" };

            var product1 = new Product() {Name = "Meat"};
            var product2 = new Product() { Name = "Fish" };
            var product3 = new Product() { Name = "Milk" };
            var product4 = new Product() { Name = "Butter" };
            var product5 = new Product() { Name = "Bread" };
            var product6 = new Product() { Name = "Water" };

            var customer1 = new Customer()
            {
                City = city3,
                CustomerName = "Aphabet of taste 2",
                WishList = new List<Product>() { product1, product2, product5}
            };

            var customer2 = new Customer()
            {
                City = city,
                CustomerName = "Shop №3",
                WishList = new List<Product>() { product4, product6 }
            };

            var customer3 = new Customer()
            {
                City = city2,
                CustomerName = "Wal Mart 1",
                WishList = new List<Product>() { product1, product2, product6 }
            };

            var customer4 = new Customer()
            {
                City = city,
                CustomerName = "Ashan 4",
                WishList = new List<Product>() { product1, product2, product5, product3, product4, product6 }
            };

            var customer5 = new Customer()
            {
                City = city2,
                CustomerName = "Shop №5",
                WishList = new List<Product>() { product6, product2, product4 }
            };

            var customer6 = new Customer()
            {
                City = city3,
                CustomerName = "Wal Mart 2",
                WishList = new List<Product>() { product2, product3, product5 }
            };

            var supplier1 = new Supplier()
            {
                SupplierName = "OO. Goods",
                City = city,
                Products = new List<Product>() {product1, product2, product5, product3},
            };

            var supplier2 = new Supplier()
            {
                SupplierName = "Food and Water comp.",
                City = city2,
                Products = new List<Product>() { product1, product2, product5, product3, product4, product6 },
            };

            var supplier3 = new Supplier()
            {
                SupplierName = "Cheronvkya inc",
                City = city2,
                Products = new List<Product>() {  product5, product3, product4, product6 },
            };

            var supplier4 = new Supplier()
            {
                SupplierName = "Maroni",
                City = city,
                Products = new List<Product>() { product1, product2, product5, product3, product4, product6 },
            };

            var supplier5 = new Supplier()
            {
                SupplierName = "Delevavro OO",
                City = city3,
                Products = new List<Product>() { product1, product2, product4, product6 },
            };

            db.Cities.AddRange(new[] {city, city2, city3});
            db.Products.AddRange(new[] {product1, product2, product3, product4, product5, product6});
            db.Customers.AddRange(new[] {customer1, customer2, customer3, customer4, customer5, customer6});
            db.Supplier.AddRange(new[] {supplier1, supplier2, supplier3, supplier4, supplier5});

            db.SaveChanges();

            base.Seed(db);
        }
    }
}
