using System.Collections.Generic;

namespace SuppliersAndConsumers.Db.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public City City { get; set; }
        public List<Product> Products { get; set; }
        public  int ProductId { get; set; }
    }
}
