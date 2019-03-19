using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using SuppliersAndConsumers.Db;

namespace SuppliersAndConsumers.ReportHandler
{
    public static class ReportHandler
    {
        private static readonly EfDbContext Context = new EfDbContext();

        public static void GenerateReportTest()
        {
            var infos = Context.Customers.Join(Context.Supplier, customer => customer.City, supplier => supplier.City,
                (customer, supplier) => new
                {
                    customer.WishList, supplier.Products, customer.City, customer.CustomerName,
                    supplier.SupplierName
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

        public static void Test()
        {
            var suppliers = Context.Supplier.ToList();
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


        public static void Test2()
        {
            var customers = Context.Customers.ToList();
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


        public static void GenerateReport()
        {
            var data = GetOrderMatchData().OrderBy(s=>s.CityName).ThenBy(s=>s.ProductName).ToList();

            int k = 0, k1 = 0, k2 = 0, k3 = 0; // Property counter 
            bool a = false, a1 = false, a2 = false, a3 = false; // Output flag
            Console.WriteLine("City \t\t |  Product \t| Suppliers \t\t |  Consumers \t\t  |");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            for (int i = 0; i < data.Count; i++)
            {
                //Console.WriteLine($"{data[i].CityName}, {data[i].ProductName}   {data[i].SupplierName} : {data[i].CustomerName} \n ----------");
                if (k == 0) k = data.Count(s => s.CityName == data[i].CityName);
                if (k1 == 0) k1 = data.Count(s => s.ProductName == data[i].ProductName && s.CityName == data[i].CityName);
                if (k2 == 0) k2 = data.Count(s => s.ProductName == data[i].ProductName && s.CityName == data[i].CityName && s.SupplierName==data[i].SupplierName);
                if (k3 == 0) k3 = data.Count(s => s.ProductName == data[i].ProductName && s.CityName == data[i].CityName && s.SupplierName == data[i].SupplierName && s.CustomerName==data[i].CustomerName);
                if (k != 0)
                {
                    if (!a) { Console.WriteLine($" {data[i].CityName} \n"); a = true; }                  
                    if (!a1) { Console.WriteLine($"\t\t\t{data[i].ProductName} \t"); a1 = true; }
                    if  (a2) Console.Write("\t\t\t\t\t");
                    if (!a2) { Console.Write($" \t\t\t\t {data[i].SupplierName} "); a2=true; }
                    if (!a3) { Console.WriteLine($" \t\t\t {data[i].CustomerName}"); a3 = true; } 
                    k--; k1--; k2--; k3--;
                    if (k == 0) a = false;
                    if (k1 == 0) a1 = false;
                    if (k2 == 0) a2 = false;
                    if (k3 == 0) a3 = false;
                }              
            }
        }

        public static void GenerateReportNew()
        {
            var data = GetOrderMatchData().OrderBy(s => s.CityName).ThenBy(s => s.ProductName).ToList();
            Console.WriteLine("City \t\t |  Product \t| Suppliers \t\t |  Consumers \t\t  |");
            Console.WriteLine("-----------------------------------------------------------------------------------");

            string lastCity=string.Empty, lastProduct=string.Empty, lastSupplier=string.Empty, lastCustomer=string.Empty;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].CityName != lastCity)
                {
                    Console.WriteLine($" {data[i].CityName} \n");
                    lastCity = data[i].CityName;
                }

                if (data[i].ProductName != lastProduct)
                {
                    Console.WriteLine($"\t\t\t{data[i].ProductName} \t");
                    lastProduct = data[i].ProductName;
                    lastSupplier = string.Empty;
                }

                if (data[i].SupplierName != lastSupplier)
                {
                    Console.Write($" \t\t\t\t {data[i].SupplierName} ");
                    lastSupplier = data[i].SupplierName;
                    lastCustomer = string.Empty;
                }
                else
                {
                    Console.Write($" \t\t\t\t\t");
                }

                if (data[i].CustomerName != lastCustomer)
                {
                    Console.WriteLine($" \t\t\t {data[i].CustomerName}");
                    lastCustomer = data[i].CustomerName;                   
                }
                else
                {
                    Console.WriteLine($" \t\t\t");
                }
            }
        }

        public static void GetRawData()
        {
            var data = GetOrderMatchData(); //.OrderBy(s => s.CityName).ThenBy(s => s.ProductName).ToList();

            
            for (int i = 0; i < data.Count; i++)
            {
                Console.WriteLine($"{data[i].CityName}, {data[i].ProductName}   {data[i].SupplierName} : {data[i].CustomerName} \n ----------");
            }
        }
        private static List<OrderMatchInfo> GetOrderMatchData()
        {
            string queryString = String.Concat(
                "SELECT Cities.CityName, Customers.CustomerName, Suppliers.SupplierName, Products.Name FROM Suppliers ",
                "JOIN Customers on Customers.CityId = Suppliers.CityId ",
                "JOIN Cities on Cities.CityId = Customers.CityId ",
                "JOIN CustomerProduct on CustomerProduct.CustomerRefId=CustomerId ",
                "JOIN SupplierProduct on SupplierProduct.SupplierRefId=SupplierId ",
                "JOIN Products on ProductId=CustomerProduct.ProductRefId AND ProductId = SupplierProduct.ProductRefId "
                );
            string connectionString = "Data source=NKOROTAEV;Initial Catalog=SuppAndConsumDB;Integrated Security=True";
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
