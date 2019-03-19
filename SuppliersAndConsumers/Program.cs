using System;
//using System.Data.Entity;
//using SuppliersAndConsumers.Db;

namespace SuppliersAndConsumers
{
    class Program
    {
        static void Main()
        {
          //  Database.SetInitializer(new EfDbContextInitializer());                             
          //  report.GenerateReport();
            //ReportHandler.Test();
            //ReportHandler.Test2();
            ////ReportHandler.GetRawData();
            ReportHandler.ReportHandler.GenerateReport();
            Console.WriteLine();
            ReportHandler.ReportHandler.GenerateReportNew();
            Console.ReadKey();
        }
    }
}
