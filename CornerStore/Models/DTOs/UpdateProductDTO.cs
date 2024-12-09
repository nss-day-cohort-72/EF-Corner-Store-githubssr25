namespace CornerStore.Models.DTOs;

public class UpdateProductDTO
{
    public string? ProductName { get; set; } // Nullable, so the client doesn't have to send it
    public decimal? Price { get; set; } // Nullable, since this is an update
    public string? Brand { get; set; } // Nullable, since this is an update
    public int? CategoryId { get; set; } // Nullable, since this is an update
    public int? Quantity { get; set; } // Nullable, since this is an update
}