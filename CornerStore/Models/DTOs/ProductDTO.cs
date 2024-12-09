namespace CornerStore.Models.DTOs;
// public class ProductDTO
// {
//     public int Id { get; set; }
//     public string ProductName { get; set; } = string.Empty;
//     public decimal Price { get; set; }
//     public string Brand { get; set; } = string.Empty;
//     public int CategoryId { get; set; }
//     public string CategoryName { get; set; } = string.Empty; // Optional for enriched data
// }

public class ProductDTO
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Brand { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty; 
}
