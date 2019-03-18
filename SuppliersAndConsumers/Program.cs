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
           // Database.SetInitializer(new EfDbContextInitializer());   
            var report = new ReportHandler();                 
          //  report.GenerateReport();
         //   report.Test();
         //   report.Test2();
              report.Test3();
            Console.ReadKey();
        }
    }
}
