namespace CornerStore.Models;

public class Order
{
    public int Id { get; set; } // Primary key
    public int CashierId { get; set; } // Foreign key
    public Cashier Cashier { get; set; } = null!; // Navigation property

    public decimal Total => OrderProducts.Sum(op => op.Product.Price * op.Quantity); // Computed property

    public DateTime? PaidOnDate { get; set; } // Nullable
    public List<OrderProduct> OrderProducts { get; set; } = new(); // Navigation property
}
