using System.Collections.Generic;

namespace SuppliersAndConsumers.Db.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public City City { get; set; }
        public List<Product> WishList { get; set; }
        public int ProductId { get; set; }
    }
}
