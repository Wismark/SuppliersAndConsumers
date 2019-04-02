using System;

namespace SuppliersAndConsumers
{
    class Program
    {
        static void Main()
        {
          //Database.SetInitializer(new EfDbContextInitializer());                             
            ReportHandler.ReportHandler.GenerateReport();
            Console.ReadKey();
        }
    }
}
