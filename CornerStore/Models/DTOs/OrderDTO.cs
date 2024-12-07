namespace CornerStore.Models.DTOs;

// public class OrderDTO
// {
//     public int Id { get; set; }
//     public int CashierId { get; set; }
//     public DateTime? PaidOnDate { get; set; }

//     public decimal Total { get; set; } // Computed total amount
//     public List<OrderProduct> OrderProducts { get; set; } = new(); // Details of products in the order
// }


public class OrderDTO
{
    public int Id { get; set; }
    public int CashierId { get; set; }
    public DateTime? PaidOnDate { get; set; }
    public decimal Total { get; set; } // Computed total amount
    public List<ProductDTO> Products { get; set; } = new(); // Products directly tied to this order
}
