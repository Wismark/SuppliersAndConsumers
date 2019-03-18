using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuppliersAndConsumers.Db.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public  virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

    }
}
