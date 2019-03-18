using SuppliersAndConsumers.Db;
using System;
using System.Data.Entity;

namespace SuppliersAndConsumers
{
    class Program
    {
        static void Main(string[] args)
        {
         //  Database.SetInitializer(new EfDbContextInitializer());   
            var report = new ReportHandler();                 
          //  report.GenerateReport();
         //  report.Test();
         //   report.Test2();
              report.Test3();
            Console.ReadKey();
        }
    }
}
