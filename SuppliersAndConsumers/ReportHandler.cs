using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuppliersAndConsumers.Db;

namespace SuppliersAndConsumers
{
    class ReportHandler
    {
        private EfDbContext _context = new EfDbContext();

        public void GenerateReport()
        {
            foreach (var product in _context.Products)
            {
                Console.WriteLine(product.Name);
            }
        }
    }
}
