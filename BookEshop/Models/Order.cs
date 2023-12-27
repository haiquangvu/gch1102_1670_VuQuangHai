namespace BookEshop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public decimal Total { get; set; }  // Total order amount
        // Other properties like customer information, order date, etc. can be added here
    }

    public class OrderLine
    {
        public int OrderLineId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}