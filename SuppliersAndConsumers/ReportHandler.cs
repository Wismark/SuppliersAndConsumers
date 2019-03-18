using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuppliersAndConsumers.Db;
using SuppliersAndConsumers.Db.Entities;

namespace SuppliersAndConsumers
{
    class ReportHandler
    {
        private readonly EfDbContext _context = new EfDbContext();

        public void GenerateReport()
        {
            var infos = _context.Customers.Join(_context.Supplier, customer => customer.City, supplier => supplier.City,
                (customer, supplier) => new
                {
                    customer.WishList, supplier.Products, customer.City, customer.CustomerName,
                    SupplierName = supplier.SupplierName
                });

            foreach (var info in infos)
            {
                var productsList = info.Products.Join(info.WishList,
                    supplierProduct => supplierProduct.ProductId,
                    customerProduct => customerProduct.ProductId,
                    (product, p2) => new {product.Name}).ToList();

                if(productsList.Any()) Console.WriteLine("City: " + info.City.CityName);
                foreach (var product in productsList)
                {
                    Console.WriteLine("Product: " + product.Name);
                    Console.WriteLine("\t Customer:" + info.CustomerName + " Supplier:" + info.SupplierName);
                }
            }
        }

        /*
                     foreach (var group in infos)
            {
                Console.WriteLine("---------------------");
                if (group.FirstOrDefault(s => s.Products.Count>0)!=null)  Console.WriteLine("City: " + group.Key.Name);              
                
                Console.WriteLine("---------------------");
            }
            foreach (var group in infos)
            {
                Console.WriteLine("---------------------");
                if (group.FirstOrDefault(s => s.Products.Count>0)!=null)  Console.WriteLine("City: " + group.Key.Name);              
                foreach (var cityGroup in group)
                {
                    var productGroup = cityGroup.Products.Join(cityGroup.WishList,
                            supplierProduct => supplierProduct.ProductId,
                            customerProduct => customerProduct.ProductId,
                            (product, p2) => new { product })
                        .GroupBy(s => s.product.Name);

                    foreach (var temp in productGroup)
                    {
                        Console.WriteLine("Product: " + temp.Key);
                        foreach (var info in temp)
                        {                                                        
                           Console.WriteLine("\t Customer:" + cityGroup.CustomerName + " Supplier:" + cityGroup.SupplierName);
                        }
                    }
                }
                Console.WriteLine("---------------------");
            }*/

        public void Test()
        {
            var suppliers = _context.Supplier.ToList();
            foreach (var data in suppliers)
            {
                Console.WriteLine("Supplier: " + data.SupplierName + "\n Products available:");
                foreach (var product in data.Products)
                {
                    Console.WriteLine(product.Name);
                }
                Console.WriteLine("---------");
            }
        }


        public void Test2()
        {
            var customers = _context.Customers.ToList();
            foreach (var data in customers)
            {
                Console.WriteLine("Customer: " + data.CustomerName + "\n Products wanted:");
                foreach (var product in data.WishList)
                {
                    Console.WriteLine(product.Name);
                }
                Console.WriteLine("---------");
            }
        }


        public void Test3()
        {
            var data = GetData().OrderBy(s=>s.CityName).ThenBy(s=>s.ProductName).ToList();

            int k = 0, k1 = 0; bool a = false, a1=false;
            Console.WriteLine("City \t\t |  Product \t| Suppliers \t\t |  Consumers \t\t  |");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            for (int i = 0; i < data.Count; i++)
            {
                //Console.WriteLine($"{data[i].CityName}, {data[i].ProductName}   {data[i].SupplierName} : {data[i].CustomerName} \n ----------");
                if (k == 0) k = data.Count(s => s.CityName == data[i].CityName);
                if (k1 == 0) k1 = data.Count(s => s.ProductName == data[i].ProductName && s.CityName == data[i].CityName);
                if (k != 0)
                {
                    if (!a) { Console.WriteLine($" {data[i].CityName} \n"); a = true; }
                    k--;
                    if (!a1) { Console.WriteLine($"\t\t\t{data[i].ProductName} \t"); a1 = true; }
                    k1--;
                    if (k == 0) a = false;
                    if (k1 == 0) a1 = false;

                    Console.WriteLine($" \t\t\t\t {data[i].SupplierName} \t\t\t {data[i].CustomerName}");
                }              
            }
        }
    public List<OrderMatchInfo> GetData()
        {
            string queryString = String.Concat(
                "SELECT Cities.CityName, Customers.CustomerName, Suppliers.SupplierName, Products.Name FROM Suppliers ",
                "JOIN Customers on Customers.City_CityId = Suppliers.City_CityId ",
                "JOIN Cities on Cities.CityId = Customers.City_CityId ",
                "JOIN CustomerProduct on CustomerProduct.CustomerRefId=CustomerId ",
                "JOIN SupplierProduct on SupplierProduct.SupplierRefId=SupplierId ",
                "JOIN Products on ProductId=CustomerProduct.ProductRefId AND ProductId = SupplierProduct.ProductRefId ",
                "ORDER BY Cities.CityName"
                );
            string connectionString = "Data Source=desktop-4c6aodq;Initial Catalog=SuppAndConsumDB;Integrated Security=True";
            var info = new List<OrderMatchInfo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                    //    Console.WriteLine($"{reader["Name"]}");
                        var item = new OrderMatchInfo()
                        {
                            CustomerName = (string) reader["CustomerName"],
                            ProductName = (string) reader["Name"],
                            SupplierName = (string) reader["SupplierName"],
                            CityName = (string)reader["CityName"]
                        };
                        info.Add(item);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return info;
        }
    }
}
