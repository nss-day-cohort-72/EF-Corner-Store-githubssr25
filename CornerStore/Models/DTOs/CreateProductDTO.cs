namespace CornerStore.Models.DTOs;

public class CreateProductDTO
{
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Brand { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}
