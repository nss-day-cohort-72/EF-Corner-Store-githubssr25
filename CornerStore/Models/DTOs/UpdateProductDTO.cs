namespace CornerStore.Models.DTOs;

public class UpdateProductDTO
{
    public string? ProductName { get; set; } // Nullable, so the client doesn't have to send it
    public decimal? Price { get; set; }
    public string? Brand { get; set; } 
    public int? CategoryId { get; set; } 
}