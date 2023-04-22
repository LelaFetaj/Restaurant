namespace Restaurant.API.Models.Entities.Orders
{
    public class Order
    {
        public Guid Id { get; set; } //guid per id unike

        public string Food { get; set; }

        public string Price { get; set; }

        public decimal Qty { get; set; }

        public string Total { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }
        
        public string CustomerName { get; set; }

        public string CustomerContact { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerAddress { get; set; }
        
    }
}
