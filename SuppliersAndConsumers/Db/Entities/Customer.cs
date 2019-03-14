namespace SuppliersAndConsumers.Db.Entities
{
    class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public City city { get; set; }
    }
}
