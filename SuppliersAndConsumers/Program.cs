using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuppliersAndConsumers.Db;

namespace SuppliersAndConsumers
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new UserDbInitializer());
            var report = new ReportHandler();
            report.GenerateReport();
            Console.WriteLine("-----");
            Console.ReadKey();
        }
    }
}
