namespace CornerStore.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; } // Composite Key Part 1
        public int ProductId { get; set; } // Composite Key Part 2
        public int Quantity { get; set; }

        public Order Order { get; set; } = null!; // Navigation Property
        public Product Product { get; set; } = null!; // Navigation Property
    }
}
