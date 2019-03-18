using System.Collections.Generic;
using System.Linq;

namespace SuppliersAndConsumers.Db.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public City City { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
