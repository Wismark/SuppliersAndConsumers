using System.Collections.Generic;
using System.Data.Entity;
using SuppliersAndConsumers.Db.Entities;

namespace SuppliersAndConsumersTest.Db
{
    public class EfDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Product> Products { get; set; }

    }

    public class UserDbInitializer : DropCreateDatabaseAlways<EfDbContext>
    {
        protected override void Seed(EfDbContext db)
        {
            var city = new City() {Name = "Москва"};
            var city2 = new City() { Name = "Воронеж" };
            var city3 = new City() {Name = "Санкт-Петербург"};

            var product1 = new Product() {Name = "Мясо"};
            var product2 = new Product() { Name = "Рыба" };
            var product3 = new Product() { Name = "Молоко" };
            var product4 = new Product() { Name = "Масло" };
            var product5 = new Product() { Name = "Хлеб" };
            var product6 = new Product() { Name = "Вода" };

            var customer1 = new Customer()
            {
                City = city3,
                CustomerName = "Азбука вкуса 2",
                WishList = new List<Product>() { product1, product2, product5}
            };

            var customer2 = new Customer()
            {
                City = city,
                CustomerName = "Ларек №3",
                WishList = new List<Product>() { product4, product6 }
            };

            var customer3 = new Customer()
            {
                City = city2,
                CustomerName = "Пятерочка 1",
                WishList = new List<Product>() { product1, product2, product6 }
            };

            var customer4 = new Customer()
            {
                City = city,
                CustomerName = "Ашан 4",
                WishList = new List<Product>() { product1, product2, product5, product3, product4, product6 }
            };

            var customer5 = new Customer()
            {
                City = city2,
                CustomerName = "Ларек №5",
                WishList = new List<Product>() { product6, product2, product4 }
            };

            var customer6 = new Customer()
            {
                City = city3,
                CustomerName = "Пятерочка 2",
                WishList = new List<Product>() { product2, product3, product5 }
            };

            var supplier1 = new Supplier()
            {
                City = city,
                Products = new List<Product>() {product1, product2, product5, product3},
            };

            var supplier2 = new Supplier()
            {
                City = city2,
                Products = new List<Product>() { product1, product2, product5, product3, product4, product6 },
            };

            var supplier3 = new Supplier()
            {
                City = city2,
                Products = new List<Product>() {  product5, product3, product4, product6 },
            };

            var supplier4 = new Supplier()
            {
                City = city,
                Products = new List<Product>() { product1, product2, product5, product3, product4, product6 },
            };

            var supplier5 = new Supplier()
            {
                City = city3,
                Products = new List<Product>() { product1, product2, product4, product6 },
            };

            base.Seed(db);
        }
    }
}
