using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SuppliersAndConsumers.ReportHandler
{
    public static class ReportHandler
    {
        public static void GenerateReport()
        {
            var orderData = GetOrderMatchData().OrderBy(s => s.CityName).ThenBy(s => s.ProductName).ToList();
            Console.WriteLine("City \t\t |  Product \t| Suppliers \t\t |  Consumers \t\t  |");
            Console.WriteLine("-----------------------------------------------------------------------------------");

            string lastCity=string.Empty, lastProduct=string.Empty, lastSupplier=string.Empty, lastCustomer=string.Empty;

            foreach (var dataRecord in orderData)
            {
                if (dataRecord.CityName != lastCity)
                {
                    Console.WriteLine($" {dataRecord.CityName} \n");
                    lastCity = dataRecord.CityName;
                }

                if (dataRecord.ProductName != lastProduct)
                {
                    Console.WriteLine($"\t\t\t{dataRecord.ProductName} \t");
                    lastProduct = dataRecord.ProductName;
                    lastSupplier = string.Empty;
                }

                if (dataRecord.SupplierName != lastSupplier)
                {
                    Console.Write($" \t\t\t\t {dataRecord.SupplierName} ");
                    lastSupplier = dataRecord.SupplierName;
                    lastCustomer = string.Empty;
                }
                else
                {
                    Console.Write($" \t\t\t\t\t");
                }

                if (dataRecord.CustomerName != lastCustomer)
                {
                    Console.WriteLine($" \t\t\t {dataRecord.CustomerName}");
                    lastCustomer = dataRecord.CustomerName;                   
                }
                else
                {
                    Console.WriteLine($" \t\t\t");
                }
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
